namespace ET
{
    public class GateUserMgrComponentDestroySystem: DestroySystem<GateUserMgrComponent>
    {
        public override void Destroy(GateUserMgrComponent self)
        {
            self.Users.Clear();
        }
    }

    [FriendClass(typeof (GateUserMgrComponent))]
    [FriendClass(typeof (AccountZoneDB))]
    [FriendClass(typeof (GateUser))]
    public static class GateUserMgrComponentSystem
    {
        public static GateUser Get(this GateUserMgrComponent self, string account)
        {
            self.Users.TryGetValue(account, out GateUser gateUser);
            return gateUser;
        }
        
        public static GateUser Create(this GateUserMgrComponent self,string account,int zone)
        {
            GateUser gateUser = self.AddChild<GateUser>();

            AccountZoneDB accountZoneDB = gateUser.AddComponent<AccountZoneDB>();
            accountZoneDB.Account = account;
            accountZoneDB.LoginZoneId = zone;
            gateUser.AddComponent<MailBoxComponent>();
            self.GetDirectDB().Save(accountZoneDB).Coroutine();
            self.Users.Add(account,gateUser);
            
            return gateUser;
        }

        public static GateUser Create(this GateUserMgrComponent self, AccountZoneDB accountZoneDB)
        {
            GateUser gateUser = self.AddChild<GateUser>();
            gateUser.AddComponent(accountZoneDB);
            gateUser.AddComponent<MailBoxComponent>();
            self.Users.Add(accountZoneDB.Account,gateUser);
            return gateUser;
        }
    }
}