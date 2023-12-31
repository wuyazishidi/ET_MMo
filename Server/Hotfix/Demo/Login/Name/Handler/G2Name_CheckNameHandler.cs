﻿using System;
using System.Collections.Generic;

namespace ET
{
    [FriendClass(typeof(CheckNameLog))]
    public class G2Name_CheckNameHandler:AMActorRpcHandler<Scene,G2Name_CheckName,Name2G_CheckName>
    {
        protected override async ETTask Run(Scene scene, G2Name_CheckName request, Name2G_CheckName response, Action reply)
        {
            if (string.IsNullOrEmpty(request.Name))
            {
                response.Error = ErrorCode.ERR_Login_NoneCheckName;
                reply();
                return;
            }

            using (await CoroutineLockComponent.Instance.Wait(CoroutineLockType.CheckName, request.Name.GetLongHashCode()))
            {
                DBComponent db = scene.GetDirectDB();
                List<CheckNameLog> list = await db.Query<CheckNameLog>(d => d.Name == request.Name);
                if (list.Count > 0)
                {
                    response.Error = ErrorCode.ERR_Login_NameRepeated;
                    reply();
                    return;
                }

                using (CheckNameLog checkNameLog = scene.GetComponent<TempComponent>().AddChild<CheckNameLog>())
                {
                    checkNameLog.Name = request.Name;
                    checkNameLog.UnitId = request.UintId;
                    checkNameLog.CreateTime = TimeHelper.ServerNow();
                    await db.Save(checkNameLog);
                }
                
                

            }

            reply();
            await ETTask.CompletedTask;
        }
    }
}