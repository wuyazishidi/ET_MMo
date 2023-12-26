namespace ET
{
    [ComponentOf(typeof(Session))]
    [ChildType(typeof(AccountDB))]
    public class RealmAccountComponent:Entity,IAwake,IDestroy
    {
        public AccountDB Info = null;
    }
}