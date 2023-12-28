
using UnityEngine;
using UnityEngine.UI;
namespace ET
{
	[ObjectSystem]
	public class DlgQueueViewComponentAwakeSystem : AwakeSystem<DlgQueueViewComponent> 
	{
		public override void Awake(DlgQueueViewComponent self)
		{
			self.uiTransform = self.GetParent<UIBaseWindow>().uiTransform;
		}
	}


	[ObjectSystem]
	public class DlgQueueViewComponentDestroySystem : DestroySystem<DlgQueueViewComponent> 
	{
		public override void Destroy(DlgQueueViewComponent self)
		{
			self.DestroyWidget();
		}
	}
}
