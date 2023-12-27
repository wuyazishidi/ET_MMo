
using UnityEngine;
using UnityEngine.UI;
namespace ET
{
	[ObjectSystem]
	public class DlgCharacterSelectViewComponentAwakeSystem : AwakeSystem<DlgCharacterSelectViewComponent> 
	{
		public override void Awake(DlgCharacterSelectViewComponent self)
		{
			self.uiTransform = self.GetParent<UIBaseWindow>().uiTransform;
		}
	}


	[ObjectSystem]
	public class DlgCharacterSelectViewComponentDestroySystem : DestroySystem<DlgCharacterSelectViewComponent> 
	{
		public override void Destroy(DlgCharacterSelectViewComponent self)
		{
			self.DestroyWidget();
		}
	}
}
