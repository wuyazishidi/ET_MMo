namespace ET
{
    public class AccountDB:Entity,IAwake,IDestroy
    {
        public string Account = default;
        public string Password = default;
    }
}