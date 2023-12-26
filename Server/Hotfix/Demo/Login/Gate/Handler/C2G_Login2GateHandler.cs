using System;
using System.Collections.Generic;

namespace ET
{
    [FriendClass(typeof(AccountZoneDB))]
    [FriendClass(typeof(RoleInfoDB))]
    [FriendClass(typeof(GateUser))]
    public class C2G_Login2GateHandler: AMRpcHandler<C2G_Login2Gate, G2C_Login2Gate>
    {
        protected override async ETTask Run(Session session, C2G_Login2Gate request, G2C_Login2Gate response, Action reply)
        {
            session.RemoveComponent<SessionAcceptTimeoutComponent>();

            Scene scene = session.DomainScene();

            GateSessionKeyComponent gateSessionKeyComponent = scene.GetComponent<GateSessionKeyComponent>();
            LoginGateInfo info = gateSessionKeyComponent.Get(request.GateKey);
            if (info == null)
            {
                response.Error = ErrorCode.ERR_Login_NoLoginGateInfo;
                reply();
                return;
            }

            string account = info.Account;
            //判断停服,维护,封号,ip,等等情况

            long instanceId = session.InstanceId;
            using (await LoginHelper.GetGateUserLock(account))
            {
                if (instanceId != session.InstanceId)
                {
                    return;
                }

                GateUserMgrComponent gateUserMgrComponent = scene.GetComponent<GateUserMgrComponent>();
                GateUser gateUser = gateUserMgrComponent.Get(account);
                if (gateUser == null)
                {
                    DBComponent db = scene.GetDirectDB();
                    List<AccountZoneDB> list = await db.Query<AccountZoneDB>(d => d.Account == account);
                    if (list.Count == 0)
                    {
                        gateUser = gateUserMgrComponent.Create(account, info.LoginZone);
                    }
                    else
                    {
                        gateUser = gateUserMgrComponent.Create(list[0]);
                    }

                    long id = gateUser.GetComponent<AccountZoneDB>().Id;
                    List<RoleInfoDB> listRole = await db.Query<RoleInfoDB>(d => d.AccountZoneId == id && !d.IsDeleted);
                    if (listRole.Count > 0)
                    {
                        foreach (RoleInfoDB roleInfoDB in listRole)
                        {
                            gateUser.GetComponent<AccountZoneDB>().AddChild(roleInfoDB);
                        }
                    }

                }
                else
                {
                    
                }
                //连接到新的Session
                gateUser.SessionInstanceId = session.InstanceId;
                reply();
            }

            await ETTask.CompletedTask;
        }
    }
}