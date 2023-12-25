
using UnityEngine;
using UnityEngine.UI;
namespace ET
{
	[ObjectSystem]
	public class DlgTapToStartViewComponentAwakeSystem : AwakeSystem<DlgTapToStartViewComponent> 
	{
		public override void Awake(DlgTapToStartViewComponent self)
		{
			self.uiTransform = self.GetParent<UIBaseWindow>().uiTransform;
		}
	}


	[ObjectSystem]
	public class DlgTapToStartViewComponentDestroySystem : DestroySystem<DlgTapToStartViewComponent> 
	{
		public override void Destroy(DlgTapToStartViewComponent self)
		{
			self.DestroyWidget();
		}
	}
}
