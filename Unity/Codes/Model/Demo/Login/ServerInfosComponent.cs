using System.Collections.Generic;

namespace ET
{
    [ChildType(typeof(ServerInfo))]
    [ComponentOf(typeof(Scene))]
    public class ServerInfosComponent:Entity,IAwake
    {
        public List<ServerInfo> ServerInfosList = new List<ServerInfo>();
    }
}