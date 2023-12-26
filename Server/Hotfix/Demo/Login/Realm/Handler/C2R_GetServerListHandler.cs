using System;

namespace ET
{
    public class C2R_GetServerListHandler:AMRpcHandler<C2R_GetServerList,R2C_GetServerList>
    {
        protected override async ETTask Run(Session session, C2R_GetServerList request, R2C_GetServerList response, Action reply)
        {
            RealmAccountComponent realmAccountComponent = session.GetComponent<RealmAccountComponent>();
            if (realmAccountComponent == null)
            {
                response.Error = ErrorCode.ERR_Login_AccountNotLogin;
                reply();
                return;
            }

            foreach (StartZoneConfig startZoneConfig in StartZoneConfigCategory.Instance.GetAll().Values)
            {
                if (startZoneConfig.ZoneType != (int)ZoneType.Game)
                {
                    continue;
                }
                response.ServerListInfos.Add(new ServerListInfo(){Zone = startZoneConfig.Id,Name = startZoneConfig.Name,Status = RandomHelper.RandomNumber(0,1)});
            }

            reply();
            await ETTask.CompletedTask;
        }
    }
}