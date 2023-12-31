using System;
using CommandLine;

namespace ET
{
    [FriendClass(typeof(RoleInfosComponent))]
    public static class LoginHelper
    {
        public static async ETTask<int> Login(Scene zoneScene, string address, string account, string password)
        {
            string url = $"http://{address}/get_realm?v={RandomHelper.RandUInt32()}";
            string result = await HttpClientHelper.Request(url);
            HTTP_GetRealmResponse httpGetRealmResponse = JsonHelper.FromJson<HTTP_GetRealmResponse>(result);
            Log.Debug($@"登录测试HTTP_GetRealmReaponse{JsonHelper.ToJson(httpGetRealmResponse)}");
            int modCount = (int)((ulong)account.GetLongHashCode() % (uint)httpGetRealmResponse.Realms.Count);
            string realmAddress = httpGetRealmResponse.Realms[modCount];
            Log.Debug($"登录测试{account}Realm:{realmAddress}");

            Session session = zoneScene.GetComponent<NetKcpComponent>().Create(NetworkHelper.ToIPEndPoint(realmAddress));
            R2C_AccountLogin r2CAccountLogin =
                    (R2C_AccountLogin)await session.Call(new C2R_AccountLogin()
                    {
                        Account = account, Password = password, LoginWay = (int)LoginWayType.Normal
                    });
            if (r2CAccountLogin.Error != ErrorCode.ERR_Success)
            {
                Log.Error($"登录测试 R2C_AccountLogin Error{r2CAccountLogin.Error}");
                return r2CAccountLogin.Error;
            }

            SessionComponent sessionComponent = zoneScene.GetComponent<SessionComponent>();
            if (null == sessionComponent)
            {
                sessionComponent = zoneScene.AddComponent<SessionComponent>();
            }

            sessionComponent.Session = session;
            return ErrorCode.ERR_Success;
        }

        public static async ETTask<int> GetServerList(Scene zoneScene)
        {
            R2C_GetServerList r2CGetServerList =
                    (R2C_GetServerList)await zoneScene.GetComponent<SessionComponent>().Session.Call(new C2R_GetServerList() { });
            if (r2CGetServerList.Error != ErrorCode.ERR_Success)
            {
                Log.Error($"获取服务器区服列表错误 R2C_GetServerList Error{r2CGetServerList.Error}");
                return r2CGetServerList.Error;
            }

            zoneScene.GetComponent<ServerInfosComponent>().ClearServerInfo();
            foreach (var serverListInfo in r2CGetServerList.ServerListInfos)
            {
                zoneScene.GetComponent<ServerInfosComponent>().AddServerInfo(serverListInfo);
            }

            return ErrorCode.ERR_Success;
        }

        public static async ETTask<int> LoginZone(Scene zoneScene, int zone)
        {
            R2C_LoginZone r2CLoginZone =
                    (R2C_LoginZone)await zoneScene.GetComponent<SessionComponent>().Session.Call(new C2R_LoginZone() { Zone = zone });
            if (r2CLoginZone.Error != ErrorCode.ERR_Success)
            {
                Log.Error($"登录测试 R2C_LoginZone Error{r2CLoginZone.Error}");
                return r2CLoginZone.Error;
            }

            Log.Debug($"登录测试 Gate:{r2CLoginZone.GateAddress}key:{r2CLoginZone.GateKey}");
            zoneScene.GetComponent<SessionComponent>().Session?.Dispose();

            Session gateSession = zoneScene.GetComponent<NetKcpComponent>().Create(NetworkHelper.ToIPEndPoint(r2CLoginZone.GateAddress));
            PingComponent pingComponent = gateSession.GetComponent<PingComponent>();
            if (pingComponent == null)
            {
                gateSession.AddComponent<PingComponent>();
            }

            zoneScene.GetComponent<SessionComponent>().Session = gateSession;

            G2C_Login2Gate g2CLogin2Gate = (G2C_Login2Gate)await gateSession.Call(new C2G_Login2Gate() { GateKey = r2CLoginZone.GateKey });
            if (g2CLogin2Gate.Error != ErrorCode.ERR_Success)
            {
                Log.Error($"登录测试 G2C_Login2Gate Error{g2CLogin2Gate.Error}");
                return g2CLogin2Gate.Error;
            }

            Log.Debug("登录Gate网关服务器成功!");
            return ErrorCode.ERR_Success;
        }

        public static async ETTask<int> GetRoleInfos(Scene zoneScene)
        {
            G2C_GetRoles g2CGetRoles = (G2C_GetRoles)await zoneScene.GetComponent<SessionComponent>().Session.Call(new C2G_GetRoles() { });
            if (g2CGetRoles.Error != ErrorCode.ERR_Success)
            {
                Log.Error($"登录测试 G2C_GetRoless Error{g2CGetRoles.Error}");
                return g2CGetRoles.Error;
            }

            zoneScene.GetComponent<RoleInfosComponent>().ClearRoleInfo();
            foreach (GeteRoleInfo geteRoleInfo in g2CGetRoles.Roles)
            {
                zoneScene.GetComponent<RoleInfosComponent>().AddRoleInfo(geteRoleInfo);
            }

            return ErrorCode.ERR_Success;
        }

        public static async ETTask<int> CreateRole(Scene zoneScene, string name)
        {
            G2C_CreateRole g2CCreateRole =
                    (G2C_CreateRole)await zoneScene.GetComponent<SessionComponent>().Session.Call(new C2G_CreateRole() { Name = name });
            if (g2CCreateRole.Error != ErrorCode.ERR_Success)
            {
                Log.Error($"登录测试 创角 Role:{g2CCreateRole.Role}");
                return g2CCreateRole.Error;
            }

            zoneScene.GetComponent<RoleInfosComponent>().AddRoleInfo(g2CCreateRole.Role);

            return ErrorCode.ERR_Success;
        }

        public static async ETTask<int> DeleteRole(Scene zoneScene, long roleId)
        {
            G2C_DeleteRole g2CDeleteRole =
                    (G2C_DeleteRole)await zoneScene.GetComponent<SessionComponent>().Session.Call(new C2G_DeleteRole() { RoleId = roleId });
            if (g2CDeleteRole.Error != ErrorCode.ERR_Success)
            {
                Log.Error($"登录测试 G2C_DeleteRole Error{g2CDeleteRole.Error}");
                return g2CDeleteRole.Error;
            }

            zoneScene.GetComponent<RoleInfosComponent>().DeleteRoleInfoById(g2CDeleteRole.RoleId);

            return ErrorCode.ERR_Success;
        }

        public static async ETTask<int> EnterMap(Scene zoneScene)
        {
            if (!zoneScene.GetComponent<RoleInfosComponent>().IsCurrentRoleExist())
            {
                return ErrorCode.ERR_Login_RoleNotExist;
            }

            G2C_Enter2Map g2CEnter2Map = (G2C_Enter2Map)await zoneScene.GetComponent<SessionComponent>().Session
                    .Call(new C2G_Enter2Map() { UnitId = zoneScene.GetComponent<RoleInfosComponent>().CurrentUnitId });
            if (g2CEnter2Map.Error == ErrorCode.ERR_Success)
            {
                Log.Error($"登录测试 G2C_Enter2Map Error{g2CEnter2Map.Error}");
                return g2CEnter2Map.Error;
            }

            zoneScene.GetComponent<PlayerComponent>().MyId = zoneScene.GetComponent<RoleInfosComponent>().CurrentUnitId;
            if (g2CEnter2Map.InQueue)
            {
                Game.EventSystem.Publish(new EventType.UpdateQueueInfo(){ZoneScene = zoneScene,Count = g2CEnter2Map.Count,Index = g2CEnter2Map.Index});
                return ErrorCode.ERR_Success;
            }
            //场景切换完成
            await zoneScene.GetComponent<ObjectWait>().Wait<WaitType.Wait_SceneChangeFinish>();
            Game.EventSystem.Publish(new EventType.EnterMapFinish(){ZoneScene = zoneScene});
            return ErrorCode.ERR_Success;
        }

        public static async ETTask<int> CancelQueue(Scene zoneScene)
        {
            G2C_CancelQueue g2CCancelQueue= (G2C_CancelQueue)await zoneScene.GetComponent<SessionComponent>().Session.Call(new C2G_CancelQueue(){UnitId = zoneScene.GetComponent<RoleInfosComponent>().CurrentUnitId});
            if (g2CCancelQueue.Error != ErrorCode.ERR_Success)
            {
                Log.Error($"取消排队 g2CCancelQueue Error{g2CCancelQueue.Error}");
                return g2CCancelQueue.Error;
            }
            return ErrorCode.ERR_Success;
        }
    }
}