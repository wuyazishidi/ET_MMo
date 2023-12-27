namespace ET
{
    public class CheckNameLogDestroySystem:DestroySystem<CheckNameLog>
    {
        public override void Destroy(CheckNameLog self)
        {
            self.Name = default;
            self.UnitId = default;
            self.CreateTime = default;
        }
    }

    public static class CheckNameLogSystem
    {
        
    }
}