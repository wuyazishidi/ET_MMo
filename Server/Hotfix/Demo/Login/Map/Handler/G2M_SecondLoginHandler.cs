using System;
using UnityEngine;

namespace ET
{
    public class G2M_SecondLoginHandler:AMActorLocationRpcHandler<Unit,G2M_SecondLogin,M2G_SecondLogin>
    {
        protected override async ETTask Run(Unit unit, G2M_SecondLogin request, M2G_SecondLogin response, Action reply)
        {
            M2C_CreateMyUnit m2CCreateMyUnit = new M2C_CreateMyUnit();
            m2CCreateMyUnit.Unit = UnitHelper.CreateUnitInfo(unit);
            MessageHelper.SendToClient(unit,m2CCreateMyUnit);
            unit.RemoveComponent<AOIEntity>();
            unit.AddComponent<AOIEntity, int, Vector3>(9 * 1000, unit.Position);

            reply();
            
            
            await ETTask.CompletedTask;
        }
    }
}