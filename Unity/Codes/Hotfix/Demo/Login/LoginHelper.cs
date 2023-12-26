using System;


namespace ET
{
    public static class LoginHelper
    
    {
        public static async ETTask<int> Login(Scene zoneScene, string address, string account, string password)
        {
            string url = $"http://{address}/get_realm?v={RandomHelper.RandUInt32()}";
            string result = await HttpClientHelper.Request(url);
            HTTP_GetRealmResponse httpGetRealmResponse = JsonHelper.FromJson<HTTP_GetRealmResponse>(result);
            Log.Debug($@"登录测试HTTP_GetRealmReaponse{JsonHelper.ToJson(httpGetRealmResponse)}");
            int modCount =Math.Abs(account.GetHashCode())%httpGetRealmResponse.Realms.Count;
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
    }
    
   
}