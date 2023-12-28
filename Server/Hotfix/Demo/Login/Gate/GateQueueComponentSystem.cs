namespace ET
{
    public  class GateQueueComponentDestroySystem:DestroySystem<GateQueueComponent>
    {
        public override void Destroy(GateQueueComponent self)
        {
            self.UnitId = default;
            self.Index = default;
            self.Count = default;
        }
    }
    
    public static class GateQueueComponentSystem
    {
        
    }
}