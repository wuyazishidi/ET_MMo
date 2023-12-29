using ET.EventType;

namespace ET
{
    public class UpdateQueueInfoEvent_RefreshQueueInfo:AEvent<UpdateQueueInfo>
    {
        protected override void Run(UpdateQueueInfo a)
        {
            a.ZoneScene.CurrentScene().GetComponent<UIComponent>().ShowWindow(WindowID.WindowID_Queue);
            a.ZoneScene.CurrentScene().GetComponent<UIComponent>().GetDlgLogic<DlgQueue>().Refresh(a.Index,a.Count);
        }
    }
}