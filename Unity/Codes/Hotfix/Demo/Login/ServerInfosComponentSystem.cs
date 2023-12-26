using System.Security.Cryptography;

namespace ET
{
    [FriendClass(typeof(ServerInfo))]
    [FriendClass(typeof(ServerInfosComponent))]
    public static class ServerInfosComponentSystem
    {
        public static void ClearServerInfo(this ServerInfosComponent self)
        {
            foreach (ServerInfo serverInfo in self.ServerInfosList)
            {
                serverInfo?.Dispose();
            }
            self.ServerInfosList.Clear();
        }
        
        public static void AddServerInfo(this ServerInfosComponent self, ServerListInfo serverListInfo)
        {
            ServerInfo serverInfo = self.AddChild<ServerInfo>();
            serverInfo.ServerZone = serverListInfo.Zone;
            serverInfo.Name = serverListInfo.Name;
            serverInfo.Status = serverListInfo.Status;
            self.ServerInfosList.Add(serverInfo);
        }
    }
    
   
}