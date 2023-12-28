namespace ET
{
    public class G2Queue_DisconnectHandler:AMActorHandler<Scene,G2Queue_DisConnect>
    {
        protected override async ETTask Run(Scene scene, G2Queue_DisConnect message)
        {
            scene.GetComponent<QueueMgrComponent>().Disconnect(message.UnitId,message.Protect);
            await ETTask.CompletedTask;
        }
    }
}