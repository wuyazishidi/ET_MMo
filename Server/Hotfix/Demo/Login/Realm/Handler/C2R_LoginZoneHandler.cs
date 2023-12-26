using System;

namespace ET
{
    [FriendClass(typeof (RealmAccountComponent))]
    [FriendClass(typeof (AccountDB))]
    public class C2R_LoginZoneHandler: AMRpcHandler<C2R_LoginZone, R2C_LoginZone>
    {
        protected override async ETTask Run(Session session, C2R_LoginZone request, R2C_LoginZone response, Action reply)
        {
            RealmAccountComponent realmAccountComponent = session.GetComponent<RealmAccountComponent>();
            if (realmAccountComponent == null)
            {
                response.Error = ErrorCode.ERR_Login_AccountNotLogin;
                reply();
                return;
            }

            if (StartSceneConfigCategory.Instance.Contain(request.Zone))
            {
                response.Error = ErrorCode.ERR_Login_ZoneNotExist;
                reply();
                return;
            }

            string account = realmAccountComponent.Info.Account;
            using (await CoroutineLockComponent.Instance.Wait(CoroutineLockType.LoginZone, account.GetHashCode()))
            {
                StartSceneConfig startZoneConfig = LoginHelper.GetGateConfig(request.Zone, account);

                G2R_GetGateKey g2RGetGateKey = (G2R_GetGateKey)await MessageHelper.CallActor(startZoneConfig.InstanceId,
                    new R2G_GetGateKey() { Info = new LoginGateInfo() { Account = account, LoginZone = request.Zone } });
                if (g2RGetGateKey.Error != ErrorCode.ERR_Success)
                {
                    response.Error = g2RGetGateKey.Error;
                    reply();
                    return;
                }

                response.GateAddress = startZoneConfig.InnerIPOutPort.ToString();
                response.GateKey = g2RGetGateKey.GateKey;
                reply();
                session?.Disconnect().Coroutine();
            }

            await ETTask.CompletedTask;
        }
    }
}