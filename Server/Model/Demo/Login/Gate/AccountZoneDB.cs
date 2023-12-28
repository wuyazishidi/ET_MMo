namespace ET
{
    [ChildType(typeof(RoleInfoDB))]
    [ComponentOf(typeof(GateUser))]
    public class AccountZoneDB:Entity,IAwake,IDestroy
    {
        public string Account;
        
        public int LoginZoneId;

        public long LastRoleId;

    }
}