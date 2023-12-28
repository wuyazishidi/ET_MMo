using System;
using System.Linq;
using System.Runtime.Serialization.Json;

namespace ET
{
    public  class QueueMgrComponentAwakeSystem:AwakeSystem<QueueMgrComponent>
    {
        public override void Awake(QueueMgrComponent self)
        {
            self.Timer_Trick = TimerComponent.Instance.NewRepeatedTimer(ConstValue.Queue_TickTime, TimerType.QueueTickTime, self);
            self.Timer_ClearProtect = TimerComponent.Instance.NewRepeatedTimer(ConstValue.Queue_ProtectTime, TimerType.QueueClearProtect, self);
            self.Timer_Update = TimerComponent.Instance.NewRepeatedTimer(ConstValue.Queue_TickUpdate, TimerType.QueueUpdateTime, self);
        }
    }
    public  class QueueMgrComponentDestroySystem:DestroySystem<QueueMgrComponent>
    {
        public override void Destroy(QueueMgrComponent self)
        {
            TimerComponent.Instance.Remove(ref self.Timer_Trick);
            TimerComponent.Instance.Remove(ref self.Timer_Update);
            TimerComponent.Instance.Remove(ref self.Timer_ClearProtect);
            
            self.Online.Clear();
            self.Protects.Clear();
            self.Queue.Clear();
        }
    }
    [Timer(TimerType.QueueTickTime)]
    public class QueueTickTime_TimerHandler:ATimer<QueueMgrComponent>
    {
        public override void Run(QueueMgrComponent t)
        {
            t.Tick();
        }
    }
    [Timer(TimerType.QueueClearProtect)]
    public class QueueClearProtect_TimerHandler:ATimer<QueueMgrComponent>
    {
        public override void Run(QueueMgrComponent t)
        {
            t.ClearProtect();
        }
    }
    [Timer(TimerType.QueueUpdateTime)]
    public class QueueUpdateTime_TimerHandler:ATimer<QueueMgrComponent>
    {
        public override void Run(QueueMgrComponent t)
        {
            t.UpdateQueue();
        }
    }

    [FriendClass(typeof(QueueMgrComponent))]
    [FriendClass(typeof(QueueInfo))]
    public static class QueueMgrComponentSystem
    {
        /// <summary>
        /// 尝试进队,返回true代表需要排队,false可以直接进游戏
        /// </summary>
        /// <param name="self"></param>
        /// <param name="account"></param>
        /// <param name="unitId"></param>
        /// <param name="gateActorId"></param>
        /// <returns></returns>
        
        public static bool TryEnqueue(this QueueMgrComponent self, string account, long unitId, long gateActorId)
        {
            if (self.Protects.ContainKey(unitId))
            {
                self.Protects.Remove(unitId);
                if (self.Queue.ContainKey(unitId))
                {
                    //排一半掉线,继续排队
                    return true;
                }
                //原本就在游戏中
                return false;
            }
            
            if (self.Online.Contains(unitId))
            {
                return false;
            }

            if (self.Queue.ContainKey(unitId))
            {
                //重复发送网络消息???按之前的位置继续排
                return true;
            }
            self.Enqueue(account,unitId,gateActorId);
            return true;
        }

        public static void Enqueue(this QueueMgrComponent self, string account, long unitId, long gateActorId)
        {
            if (self.Queue.ContainKey(unitId))
            {
                return;
            }

            QueueInfo queueInfo = self.AddChild<QueueInfo>();
            queueInfo.Account = account;
            queueInfo.UnitId = unitId;
            queueInfo.GateActorId = gateActorId;
            queueInfo.Index = self.Queue.Count + 1;
            self.Queue.AddLast(unitId, queueInfo);
        }

        public static int GetIndex(this QueueMgrComponent self, long unitId)
        {
            return self.Queue[unitId]?.Index??1;
        }

        public static void Tick(this QueueMgrComponent self)
        {
            if (self.Online.Count >= ConstValue.Queue_MaxOnline)
            {
                //满人就不放入玩家
                return;
            }

            for (int i = 0; i < ConstValue.Queue_TickeCount; i++)
            {
                if (self.Queue.Count <= 0)
                {
                    return;
                }

                QueueInfo queueInfo = self.Queue.First;
                self.EnterMap(queueInfo.UnitId).Coroutine();
                
            }
        }

        public static async ETTask EnterMap(this QueueMgrComponent self, long unitId)
        {
            if (!self.Online.Add(unitId))
            {
                return;
            }

            QueueInfo queueInfo = self.Queue.Remove(unitId);
            if (queueInfo != null)
            {
                G2Queue_EnterMap g2QueueEnqueue = (G2Queue_EnterMap)await MessageHelper.CallActor(queueInfo.GateActorId,
                    new Queue2G_EnterMap() { Account = queueInfo.Account, UnitId = queueInfo.UnitId });

                if (g2QueueEnqueue.NeedRemove)
                {
                    self.Online.Remove(unitId);
                }
                queueInfo.Dispose();
            }
            await ETTask.CompletedTask;
        }

        public static void UpdateQueue(this QueueMgrComponent self)
        {
            using (DictionaryPoolComponent<long, Queue2G_UpdateInfo> dict = DictionaryPoolComponent<long, Queue2G_UpdateInfo>.Create())
            {
                using (var enumrator = self.Queue.GetEnumrator())
                {
                    int i = 1;
                    while (enumrator.MoveNext())
                    {
                        QueueInfo queueInfo = enumrator.Current;
                        queueInfo.Index = i;
                        ++i;

                        Queue2G_UpdateInfo queue2GUpdateInfo;
                        if (!dict.TryGetValue(queueInfo.GateActorId, out queue2GUpdateInfo))
                        {
                            queue2GUpdateInfo = new Queue2G_UpdateInfo() { Count = self.Queue.Count };
                            dict.Add(queueInfo.GateActorId,queue2GUpdateInfo);
                        }

                        queue2GUpdateInfo.Account.Add(queueInfo.Account);
                        queue2GUpdateInfo.Index.Add(queueInfo.Index);
                    }

                    foreach (var info in dict)
                    {
                        MessageHelper.SendActor(info.Key, info.Value);
                    }
                }
            }
        }

        public static void Disconnect(this QueueMgrComponent self, long unitId, bool isProtect)
        {
            if (isProtect)
            {
                if (self.Online.Contains(unitId) || self.Queue.ContainKey(unitId))
                {
                    //进入掉线保护状态
                    self.Protects.AddLast(unitId, new ProtectInfo() { UnitId = unitId, Time = TimeHelper.ServerNow() });
                } 
            }
            else
            {
                self.Online.Remove(unitId);
                self.Queue.Remove(unitId);
                self.Protects.Remove(unitId);
            }
        }

        public static void ClearProtect(this QueueMgrComponent self)
        {
            long targetTime = TimeHelper.ServerNow() - ConstValue.Queue_ProtectTime;
            while (self.Protects.Count > 0)
            {
                ProtectInfo protectInfo = self.Protects.First;
                if(self.Protects.First.Time>targetTime)
                {
                    break;
                }
                self.Disconnect(protectInfo.UnitId,false);
            }
        }
    }
}