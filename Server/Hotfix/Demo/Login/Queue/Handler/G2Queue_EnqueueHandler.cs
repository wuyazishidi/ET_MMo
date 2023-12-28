using System;

namespace ET
{
    public class G2Queue_EnqueueHandler:AMActorRpcHandler<Scene ,G2Queue_Enqueue,Queue2G_Equeue>
    {
        protected override async ETTask Run(Scene scene, G2Queue_Enqueue request, Queue2G_Equeue response, Action reply)
        {
            QueueMgrComponent queueMgrComponent = scene.GetComponent<QueueMgrComponent>();
            if (queueMgrComponent.TryEnqueue(request.Account, request.UnitId, request.GateActorId))
            {
                response.NeedQueue = true;
                response.Count = queueMgrComponent.Queue.Count;
                response.Index = queueMgrComponent.GetIndex(request.UnitId);
            }

            reply();

            await ETTask.CompletedTask;
        }
    }
}