namespace ET
{
    [ComponentOf(typeof(GateUser))]
    public class GateUserDisconnectComponent:Entity,IAwake<long>,IDestroy
    {
        public long Timer;
    }
}