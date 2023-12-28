using System;

namespace ET
{
    [FriendClassAttribute(typeof(ET.RoleInfoDB))]
    public class C2G_DeleteRoleHandler : AMRpcHandler<C2G_DeleteRole, G2C_DeleteRole>
    {
        protected override async ETTask Run(Session session, C2G_DeleteRole request, G2C_DeleteRole response, Action reply)
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

            if (!accountZoneDB.Children.ContainsKey(request.RoleId))
            {
                response.Error = ErrorCode.ERR_Login_NoRole;
                reply();
                return;
            }

            long instanceId = accountZoneDB.InstanceId;
            using (await gateUser.GetGateUserLock())
            {
                if (instanceId != accountZoneDB.InstanceId)
                {
                    response.Error = ErrorCode.ERR_Login_NoneAccountZone;
                    reply();
                    return;
                }

                RoleInfoDB roleInfoDB = accountZoneDB.Children[request.RoleId] as RoleInfoDB; ;
                if (roleInfoDB == null)
                {
                    response.Error = ErrorCode.ERR_Login_NoRoleDB;
                    reply();
                    return;
                }

                roleInfoDB.IsDeleted = true;
                DBComponent db = session.GetDirectDB();
                await db.Save(roleInfoDB);
                response.RoleId = roleInfoDB.Id;
                roleInfoDB.Dispose();

            }

            reply();
            await ETTask.CompletedTask;

        }
    }
}