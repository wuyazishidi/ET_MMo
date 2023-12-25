using System;
using System.Collections.Generic;

namespace ET
{
    [FriendClass(typeof(AccountDB))]
    public class C2R_AccountLoginHandler: AMRpcHandler<C2R_AccountLogin, R2C_AccountLogin>
    {
        protected override async ETTask Run(Session session, C2R_AccountLogin request, R2C_AccountLogin response, Action reply)
        {
            session.RemoveComponent<SessionAcceptTimeoutComponent>();
            int modCount = request.Account.GetHashCode() % StartSceneConfigCategory.Instance.Realms.Count;
            if (session.DomainScene().InstanceId != StartSceneConfigCategory.Instance.Realms[modCount].InstanceId)
            {
                response.Error = ErrorCode.ERR_RealmAddressError;
                reply();
                session?.Disconnect().Coroutine();
                return;
            }

            if (string.IsNullOrEmpty(request.Account) || string.IsNullOrEmpty(request.Password))
            {
                response.Error = ErrorCode.ERR_Login_AccountError;
                reply();
                return;
            }

            string account = request.Account;
            
            AccountDB accountDB = null;
           // DBManagerComponent.Instance.GetZoneDB();//每次调用都要传入zoneID,所以不便
           List<AccountDB> list = await session.GetDirectDB().Query<AccountDB>(db => db.Account == account);
           if (list.Count > 0)//
           {
               accountDB = list[0];
           }

           if (Game.Options.Develop == 0)
           {
               
           }
            await ETTask.CompletedTask;
        }
    }
}