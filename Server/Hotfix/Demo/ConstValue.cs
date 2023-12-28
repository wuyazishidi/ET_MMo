namespace ET
{
    public static class ConstValue
    {
        public const int Login_GateUserDisconnectTime = 20*1000;//顶号保留时间
        
        public const int Login_SessionDisconnectTime = 60*1000;//断线保留时间
        
        public const int Queue_MaxOnline =3000;//最大在线人数
        
        public const int Queue_TickTime =1*1000;//间隔放人时间

        public const int Queue_TickeCount = 1;//每次放几个人

        public const int Queue_TickUpdate = 10 * 1000;//更新排名的时间 间隔

        public const int Queue_ClearProtect = 10 * 1000;//掉线保护检测间隔
        
        public const int Queue_ProtectTime = 5*60 * 1000;//掉线保护时长
        
    }
}