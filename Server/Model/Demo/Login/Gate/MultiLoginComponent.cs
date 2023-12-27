namespace ET
{
    /// <summary>
    /// 顶号和多次登录的标识
    /// </summary>
    [ComponentOf(typeof(GateUser))]
    public class MultiLoginComponent:Entity,IAwake,IDestroy
    {
        public long Timer_Over;

    }
}