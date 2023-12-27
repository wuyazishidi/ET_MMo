
using UnityEngine;
using UnityEngine.UI;
namespace ET
{
	[ObjectSystem]
	public class Scroll_Item_characterDestroySystem : DestroySystem<Scroll_Item_character> 
	{
		public override void Destroy( Scroll_Item_character self )
		{
			self.DestroyWidget();
		}
	}
}
