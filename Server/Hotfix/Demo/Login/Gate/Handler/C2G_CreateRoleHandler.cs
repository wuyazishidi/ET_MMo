using System;

namespace ET
{
    [FriendClassAttribute(typeof (ET.AccountZoneDB))]
    [FriendClassAttribute(typeof (ET.RoleInfoDB))]
    public class C2G_CreateRoleHandler: AMRpcHandler<C2G_CreateRole, G2C_CreateRole>
    {
        protected override async ETTask Run(Session session, C2G_CreateRole request, G2C_CreateRole response, Action reply)
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

            if (string.IsNullOrEmpty(request.Name))
            {
                response.Error = ErrorCode.ERR_Login_NoneName;
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

                long unitId = IdGenerater.Instance.GenerateUnitId(accountZoneDB.LoginZoneId);

                Name2G_CheckName name2GCheckName = (Name2G_CheckName)await MessageHelper.CallActor(accountZoneDB.LoginZoneId, SceneType.Name,
                    new G2Name_CheckName() { Name = request.Name, UintId = unitId });
                if (name2GCheckName.Error != ErrorCode.ERR_Success)
                {
                    response.Error = name2GCheckName.Error;
                    reply();
                    return;
                }

                RoleInfoDB roleInfoDB = accountZoneDB.AddChildWithId<RoleInfoDB>(unitId);
                roleInfoDB.Account = accountZoneDB.Account;
                roleInfoDB.AccountZoneId = accountZoneDB.Id;
                roleInfoDB.LogicZone = accountZoneDB.LoginZoneId;
                roleInfoDB.IsDeleted = false;
                roleInfoDB.Name = request.Name;
                roleInfoDB.Level = 1;

                await session.GetDirectDB().Save(roleInfoDB);
                response.Role = roleInfoDB.ToMessage();
            }

            reply();

            await ETTask.CompletedTask;
        }
    }
}