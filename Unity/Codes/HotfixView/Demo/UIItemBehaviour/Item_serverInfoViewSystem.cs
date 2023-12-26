
using UnityEngine;
using UnityEngine.UI;
namespace ET
{
	[ObjectSystem]
	public class Scroll_Item_serverInfoDestroySystem : DestroySystem<Scroll_Item_serverInfo> 
	{
		public override void Destroy( Scroll_Item_serverInfo self )
		{
			self.DestroyWidget();
		}
	}
}
