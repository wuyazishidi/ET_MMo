namespace ET
{
    public class MultiLoginComponentAwakeSystem:AwakeSystem<MultiLoginComponent>
    {
        public override void Awake(MultiLoginComponent self)
        {
            self.Timer_Over = TimerComponent.Instance.NewOnceTimer(TimeHelper.ServerNow() + (20 * 1000), TimerType.MultiLogin, self);
        }
    }
    
    public class MultiLoginComponentDestroySystem:DestroySystem<MultiLoginComponent>
    {
        public override void Destroy(MultiLoginComponent self)
        {
            TimerComponent.Instance.Remove(ref self.Timer_Over);
        }
    }

    [Timer(TimerType.MultiLogin)]
    public class MultiLoginComponent_TimerHandler:ATimer<MultiLoginComponent>
    {
        public override void Run(MultiLoginComponent t)
        {
            t.GetParent<GateUser>()?.OfflineWithLock(false).Coroutine();
        }
    }

    public static class MultiLoginComponentSystem
    {
        
    }
}