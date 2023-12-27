using System;
using System.Diagnostics.SymbolStore;

namespace ET
{
    [FriendClass(typeof(GateUserMgrComponent))]
    [FriendClass(typeof(GateUser))]
    public class R2G_GetGateKeyHandler: AMActorRpcHandler<Scene, R2G_GetGateKey, G2R_GetGateKey>
    {
        protected override async ETTask Run(Scene scene, R2G_GetGateKey request, G2R_GetGateKey response, Action reply)
        {
            GateUserMgrComponent gateUserMgrComponent = scene.GetComponent<GateUserMgrComponent>();
            gateUserMgrComponent.Users.TryGetValue(request.Info.Account, out GateUser gateUser);
            if (gateUser != null)
            {
                //TODO 执行下线顶号操作
                long instanceId = gateUser.InstanceId;
                using (await gateUser.GetGateUserLock())
                {
                    if (instanceId != gateUser.InstanceId)
                    {
                        reply();
                        return;
                    }

                    gateUser.OfflineSession();
                }
            }

            GateSessionKeyComponent gateSessionKeyComponent = scene.GetComponent<GateSessionKeyComponent>();
            long key = RandomHelper.RandInt64();
            gateSessionKeyComponent.Add(key, request.Info);
            response.GateKey = key;
            reply();
            await ETTask.CompletedTask;
        }
    }
}