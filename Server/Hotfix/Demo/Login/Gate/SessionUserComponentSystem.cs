namespace ET
{
    public class SessionUserComponentAwakeSystem: AwakeSystem<SessionUserComponent, long>
    {
        public override void Awake(SessionUserComponent self, long a)
        {
            self.GateUserInstanceId = a;
        }
    }

    public class SessionUserComponentDestroySystem: DestroySystem<SessionUserComponent>
    {
        public override void Destroy(SessionUserComponent self)
        {
            GateUser gateUser = self.User;
            if (gateUser != null && self.GetParent<Session>().IsDisposed)
            {
                //如果是主动断开,应该先移除SessionUserComponent,再销毁Session,否则就认为是突然断开了
                //Session突然断开,一段时间没重连就下线
                gateUser.AddComponent<GateUserDisconnectComponent, long>(ConstValue.Login_SessionDisconnectTime);
            }

            self.GateUserInstanceId = 0;
        }
    }

    public static class SessionUserComponentSystem
    {
    }
}