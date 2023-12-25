
using UnityEngine;
using UnityEngine.UI;
namespace ET
{
	[ObjectSystem]
	public class DlgAccountLoginViewComponentAwakeSystem : AwakeSystem<DlgAccountLoginViewComponent> 
	{
		public override void Awake(DlgAccountLoginViewComponent self)
		{
			self.uiTransform = self.GetParent<UIBaseWindow>().uiTransform;
		}
	}


	[ObjectSystem]
	public class DlgAccountLoginViewComponentDestroySystem : DestroySystem<DlgAccountLoginViewComponent> 
	{
		public override void Destroy(DlgAccountLoginViewComponent self)
		{
			self.DestroyWidget();
		}
	}
}
