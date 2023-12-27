
using UnityEngine;
using UnityEngine.UI;
namespace ET
{
	[ObjectSystem]
	public class DlgCreateCharacterViewComponentAwakeSystem : AwakeSystem<DlgCreateCharacterViewComponent> 
	{
		public override void Awake(DlgCreateCharacterViewComponent self)
		{
			self.uiTransform = self.GetParent<UIBaseWindow>().uiTransform;
		}
	}


	[ObjectSystem]
	public class DlgCreateCharacterViewComponentDestroySystem : DestroySystem<DlgCreateCharacterViewComponent> 
	{
		public override void Destroy(DlgCreateCharacterViewComponent self)
		{
			self.DestroyWidget();
		}
	}
}
