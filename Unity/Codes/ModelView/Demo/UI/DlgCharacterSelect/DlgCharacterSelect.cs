using System.Collections.Generic;

namespace ET
{
	 [ComponentOf(typeof(UIBaseWindow))]
	public  class DlgCharacterSelect :Entity,IAwake,IUILogic
	{

		public DlgCharacterSelectViewComponent View { get => this.Parent.GetComponent<DlgCharacterSelectViewComponent>();}

		public Dictionary<int, Scroll_Item_character> ScrollItemCharacters;

	}
}
