namespace ET
{
    public class AccountDBDestroySystem:DestroySystem<AccountDB>
    {
        public override void Destroy(AccountDB self)
        {
            self.Account = null;
            self.Password = null;
        }
    }
}