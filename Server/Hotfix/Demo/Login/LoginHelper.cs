using System;

namespace ET
{
    public static class LoginHelper
    {
        public static async ETTask<CoroutineLock> GetGateUserLock(string account)
        {
            if (string.IsNullOrEmpty(account))
            {
                throw new Exception("GetGateUserLock but account is NuLL !");
            }

            return await CoroutineLockComponent.Instance.Wait(CoroutineLockType.GateUserLock, account.GetHashCode());
            
        }

        public static async ETTask Disconnect(this Session self)
        {
            if (self == null)
            {
                return;
            }

            long instanceId = self.InstanceId;
            await TimerComponent.Instance.WaitAsync(1000);
            if (instanceId != self.InstanceId)
            {
                return;
            }
            
            self.Dispose();
        }

        public static StartSceneConfig GetGateConfig(int zone,string account)
        {
            int modCount = Math.Abs(account.GetHashCode()) % StartSceneConfigCategory.Instance.Gates[zone].Count;
            StartSceneConfig gateConfig = StartSceneConfigCategory.Instance.Gates[zone][modCount];
            return gateConfig;
        }
    }
}