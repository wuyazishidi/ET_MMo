namespace ET
{
    [ChildType(typeof(AccountZoneDB))]
    [ComponentOf(typeof(GateUser))]
    public class RoleInfoDB:Entity,IAwake,IDestroy
    {
        public string Account;
        
        public long AccountZoneId;
        
        
        public bool IsDeleted;

        public string Name;
        
        public int Level;

        public int LogicZone;//逻辑区服
    }
}