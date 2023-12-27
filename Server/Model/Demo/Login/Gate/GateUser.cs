namespace ET
{
    public enum GateUserState
    {
        InGate=1,
        InQueue=2,
        InMap=3,
    }
    public class GateUser:Entity,IAwake,IDestroy
    {
        public long SessionInstanceId;
        public Session Session => Game.EventSystem.Get(this.SessionInstanceId)as Session;
        public GateUserState State = GateUserState.InGate;

    }
}