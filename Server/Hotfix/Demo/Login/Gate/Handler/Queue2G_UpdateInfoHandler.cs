namespace ET
{
    [FriendClassAttribute(typeof(ET.GateUser))]
    [FriendClassAttribute(typeof(ET.GateQueueComponent))]
    public class Queue2G_UpdateInfoHandler : AMActorHandler<Scene, Queue2G_UpdateInfo>
    {
        protected override async ETTask Run(Scene scene, Queue2G_UpdateInfo message)
        {
            if (message.Account.Count != message.Index.Count)
            {
                return;
            }

            G2C_UpdateQueue g2CUpdateQueue = new G2C_UpdateQueue(){Count = message.Count};
            GateUserMgrComponent gateUserMgrComponent = scene.GetComponent<GateUserMgrComponent>();
            for (int i = 0; i < message.Account.Count; i++)
            {
                string accoount = message.Account[i];
                GateUser gateUser = gateUserMgrComponent.Get(accoount);
                if (gateUser == null || gateUser.State != GateUserState.InQueue)
                {
                    continue;
                }

                GateQueueComponent gateQueueComponent = gateUser.GetComponent<GateQueueComponent>();
                gateQueueComponent.Index = message.Index[i];
                gateQueueComponent.Count = message.Count;

                g2CUpdateQueue.Index = message.Index[i];
                gateUser.Session.Send(g2CUpdateQueue);
                if ((i + 1) % 5 == 0)
                {
                    await TimerComponent.Instance.WaitFrameAsync();
                }
            }
            await ETTask.CompletedTask;
        }
    }
}