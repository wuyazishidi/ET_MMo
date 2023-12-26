using System.Collections.Generic;

namespace ET
{
	[ComponentOf(typeof(Scene))]
	public class GateSessionKeyComponent : Entity, IAwake
	{
		public readonly Dictionary<long, LoginGateInfo> sessionKey = new Dictionary<long, LoginGateInfo>();
	}
}
