using System;

namespace ET
{
    public class C2G_GetRolesHandler:AMRpcHandler<C2G_GetRoles,G2C_GetRoles>
    {
        protected override async ETTask Run(Session session, C2G_GetRoles request, G2C_GetRoles response, Action reply)
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

            if (accountZoneDB.Children.Count > 0)
            {
                foreach (Entity entity in accountZoneDB.Children.Values)
                {
                    if (entity is RoleInfoDB roleInfoDB)
                    {
                        response.Roles.Add(roleInfoDB.ToMessage());
                    }
                }
            }

            reply();
            await ETTask.CompletedTask;
        }
    }
}