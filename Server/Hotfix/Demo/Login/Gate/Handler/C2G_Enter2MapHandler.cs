using System;
using MongoDB.Driver.Linq;

namespace ET
{
    [FriendClassAttribute(typeof(ET.AccountZoneDB))]
    [FriendClassAttribute(typeof(ET.RoleInfoDB))]
    [FriendClassAttribute(typeof(ET.GateUser))]
    [FriendClassAttribute(typeof(ET.GateQueueComponent))]
    public class C2G_Enter2MapHandler : AMRpcHandler<C2G_Enter2Map, G2C_Enter2Map>
    {
        protected override async ETTask Run(Session session, C2G_Enter2Map request, G2C_Enter2Map response, Action reply)
        {
            GateUser gateUser = session.GetComponent<SessionUserComponent>()?.User;
            if (gateUser == null)
            {
                response.Error = ErrorCode.ERR_Login_NoneGateUser;
                reply();
                return;
            }

            AccountZoneDB accountZoneDB = gateUser.GetComponent<AccountZoneDB>();
            if (accountZoneDB == null)
            {
                response.Error = ErrorCode.ERR_Login_NoneAccountZone;
                reply();
                return;
            }

            long instanceId = accountZoneDB.InstanceId;
            long unitId = request.UnitId;
            string account = accountZoneDB.Account;
            using (await gateUser.GetGateUserLock())
            {
                if (instanceId != accountZoneDB.InstanceId)
                {
                    response.Error = ErrorCode.ERR_Login_NoneAccountZone;
                    reply();
                    return;
                }

                if (gateUser.GetComponent<MultiLoginComponent>() != null)
                {
                    if (accountZoneDB.LastRoleId != unitId)
                    {
                        await gateUser.Offline(false);
                    }
                    
                    //等上面下线后再移除顶号状态,防止这时候刚好排队服排到了上一个号
                    gateUser.RemoveComponent<MultiLoginComponent>();

                    if (gateUser.State == GateUserState.InQueue)
                    {
                        GateQueueComponent gateQueueComponent = gateUser.GetComponent<GateQueueComponent>();
                        response.InQueue = true;
                        response.Index = gateQueueComponent.Index;
                        response.Count = gateQueueComponent.Count;
                        reply();
                        return;
                    }

                    if (gateUser.State == GateUserState.InMap)
                    {
                        reply();
                        gateUser.EnterMap().Coroutine();
                        return;
                    }
                }
                
                

                RoleInfoDB targetRoleInfoDB = accountZoneDB.GetChild<RoleInfoDB>(unitId);
                if (targetRoleInfoDB == null || targetRoleInfoDB.IsDeleted)
                {
                    response.Error = ErrorCode.ERR_Login_NoRoleDB;
                    reply();
                    return;
                }

                //正常的选角流程
                accountZoneDB.LastRoleId = unitId;

                Queue2G_Equeue queue2GEqueue = (Queue2G_Equeue)await MessageHelper.CallActor(accountZoneDB.LoginZoneId, SceneType.Queue,
                    new G2Queue_Enqueue() { Account = account, UnitId = unitId, GateActorId = session.DomainScene().InstanceId });
                
                
                if (queue2GEqueue.Error != ErrorCode.ERR_Success)
                {
                    response.Error = queue2GEqueue.Error;
                    reply();
                    return;
                }

                response.InQueue = queue2GEqueue.NeedQueue;
                if (queue2GEqueue.NeedQueue)
                {
                    gateUser.State = GateUserState.InQueue;
                    GateQueueComponent gateQueueComponent = gateUser.GetComponent<GateQueueComponent>();
                    if (gateQueueComponent == null)
                    {
                        gateQueueComponent = gateUser.AddComponent<GateQueueComponent>();
                    }

                    gateQueueComponent.UnitId = unitId;
                    gateQueueComponent.Index = queue2GEqueue.Index;
                    gateQueueComponent.Count = queue2GEqueue.Count;
                    response.Index = queue2GEqueue.Index;
                    response.Count = queue2GEqueue.Count;
                    Log.Console($"->测试 账号{account}需要排队{gateQueueComponent.Index}{gateQueueComponent.Count}");
                }
                reply();
                DBComponent db = session.GetDirectDB();
                await db.Save(accountZoneDB);
                if (queue2GEqueue.NeedQueue)
                {
                    Log.Console($"->测试 账号{account}免排队直接进游戏");
                    //游戏角色直接游戏
                    gateUser.EnterMap().Coroutine();
                }
                

            }
            await ETTask.CompletedTask;
        }
    }
}