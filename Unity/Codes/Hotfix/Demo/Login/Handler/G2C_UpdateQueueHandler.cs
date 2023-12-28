using ET.EventType;

namespace ET
{
    public class G2C_UpdateQueueHandler:AMHandler<G2C_UpdateQueue>
    {
        protected override void Run(Session session, G2C_UpdateQueue message)
        {
            Game.EventSystem.Publish(new UpdateQueueInfo(){ZoneScene = session.ZoneScene(),Index = message.Index,Count = message.Count});
        }
    }
}