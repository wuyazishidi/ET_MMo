using System;


namespace ET
{
    public static class LoginHelper
    
    {
        public static async ETTask Login(Scene zoneScene, string address, string account, string password)
        {
            string url = $"http://{address}/get_realm?v={RandomHelper.RandUInt32()}";
            string result = await HttpClientHelper.Request(url);
            HTTP_GetRealmResponse httpGetRealmResponse = JsonHelper.FromJson<HTTP_GetRealmResponse>(result);
            Log.Debug($@"登录测试HTTP_GetRealmReaponse{JsonHelper.ToJson(httpGetRealmResponse)}");
            int modCount = account.GetHashCode() % httpGetRealmResponse.Realms.Count;
            string realmAddress = httpGetRealmResponse.Realms[modCount];
            
        } 
    }
}