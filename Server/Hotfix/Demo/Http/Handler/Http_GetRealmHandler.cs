using System.Net;

namespace ET
{
    [HttpHandler(SceneType.RealmInfo,"/get_realm")]
    public class Http_GetRealmHandler:IHttpHandler
    {
        public async ETTask Handle(Entity domain, HttpListenerContext context)
        {

            HTTP_GetRealmResponse httpGetRealmResponse = new HTTP_GetRealmResponse();
            foreach (StartSceneConfig startSceneConfig in StartSceneConfigCategory.Instance.Realms)
            {
                httpGetRealmResponse.Realms.Add(startSceneConfig.InnerIPOutPort.ToString());

            }
            HttpHelper.Response(context,httpGetRealmResponse);
            await ETTask.CompletedTask;
        }
    }
}