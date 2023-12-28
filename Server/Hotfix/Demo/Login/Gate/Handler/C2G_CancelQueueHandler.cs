using System;

namespace ET
{
    [FriendClassAttribute(typeof(ET.GateUser))]
    [FriendClassAttribute(typeof(ET.AccountZoneDB))]
    public class C2G_CancelQueueHandler : AMRpcHandler<C2G_CancelQueue, G2C_CancelQueue>
    {
        protected override async ETTask Run(Session session, C2G_CancelQueue request, G2C_CancelQueue response, Action reply)
        {
            GateUser gateUser = session.GetComponent<SessionUserComponent>()?.User;
            if (gateUser == null)
            {
                response.Error = ErrorCode.ERR_Login_NoneGateUser;
                reply();
                return;
            }

            AccountZoneDB accountZoneDB = gateUser.GetComponent<AccountZoneDB>();
            if (accountZoneDB == null)
            {
                response.Error = ErrorCode.ERR_Login_NoneAccountZone;
                reply();
                return;
            }

            long instanceId = gateUser.InstanceId;
            using (await gateUser.GetGateUserLock())
            {
                if (instanceId != gateUser.InstanceId)
                {
                    response.Error = ErrorCode.ERR_Login_NoneAccountZone;
                    reply();
                    return;
                }

                if (gateUser.State == GateUserState.InMap)
                {
                    response.Error = ErrorCode.ERR_Login_RoleInMap;
                    reply();
                    return;
                }

                gateUser.RemoveComponent<GateQueueComponent>();

                MessageHelper.SendActor(session.DomainZone(), SceneType.Zone, new G2Queue_DisConnect() { UnitId = accountZoneDB.LastRoleId ,Protect = false});
            }

            reply();
            
            await ETTask.CompletedTask;
        }
    }
}