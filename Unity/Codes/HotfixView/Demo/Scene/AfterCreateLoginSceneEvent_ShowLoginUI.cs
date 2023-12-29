using ET.EventType;

namespace ET
{
    public class AfterCreateLoginSceneEvent_ShowLoginUI:AEvent<AfterCreateLoginScene>
    {
        protected override void Run(AfterCreateLoginScene a)
        {
            a.LoginScene.GetComponent<UIComponent>().ShowWindow(WindowID.WindowID_TapToStart);
            a.LoginScene.GetComponent<UIComponent>().ShowWindow(WindowID.WindowID_AccountLogin);
        }
    }
}