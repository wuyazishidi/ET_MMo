namespace ET
{
    [ChildType(typeof(AccountZoneDB))]
    [ComponentOf(typeof(GateUser))]
    public class RoleInfoDB:Entity,IAwake,IDestroy
    {
        public string Account;
        
        public int AccountZoneId;
        
        
        public bool IsDeleted;

        public string Name;
        
        public int level;
    }
}