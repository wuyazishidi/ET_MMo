namespace ET
{
    public class RealmAccountComponentDestroySystem:DestroySystem<RealmAccountComponent>
    {
        public override void Destroy(RealmAccountComponent self)
        {
            self.Info = null;
        }
    }
}