using System.Collections.Generic;
using System.Linq.Expressions;

namespace ET
{
    public class QueueInfo: Entity, IAwake, IDestroy
    {
        public long UnitId;
        public long GateActorId;
        public string Account;
        public int Index;
        
        //这里可以放玩家的等级,VIP等级,ip,权限等等
    }

    public struct ProtectInfo
    {
        public long UnitId;
        public long Time;
    }
    
    [ComponentOf(typeof(Scene))]
    [ChildType(typeof(QueueInfo))]
    public class QueueMgrComponent:Entity,IAwake,IDestroy
    {
        //允许在线的玩家
        public HashSet<long> Online = new HashSet<long>();
        
        //排队队列
        public HashLinkedList<long, QueueInfo> Queue = new HashLinkedList<long, QueueInfo>();
        
        //掉线保护的玩家
        public HashLinkedList<long, ProtectInfo> Protects = new HashLinkedList<long, ProtectInfo>();

        public long Timer_Trick;//排队检测放入玩家进入Map服务器

        public long Timer_ClearProtect;//定时清除保护的信息

        public long Timer_Update;//排队更新排名
    }
}