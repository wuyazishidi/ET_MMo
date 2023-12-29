using ET.EventType;

namespace ET
{
    public class AppStartInitFinishEvent_CreateLoginScene:AEvent<AppStartInitFinish>
    {
        protected override void Run(AppStartInitFinish a)
        {
            Scene zoneScene = a.ZoneScene;

            CurrentScenesComponent currentScenesComponent = zoneScene.GetComponent<CurrentScenesComponent>();
            currentScenesComponent.Scene?.Dispose();
            Scene loginScene = SceneFactory.CreateCurrentScene(IdGenerater.Instance.GenerateId(), zoneScene.Zone, "loginScene",currentScenesComponent);
            Game.EventSystem.Publish(new AfterCreateLoginScene() { LoginScene = loginScene });
            
        }
    }
}