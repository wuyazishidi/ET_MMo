namespace ET
{
	 [ComponentOf(typeof(UIBaseWindow))]
	public  class DlgCreateCharacter :Entity,IAwake,IUILogic
	{

		public DlgCreateCharacterViewComponent View { get => this.Parent.GetComponent<DlgCreateCharacterViewComponent>();} 

		 

	}
}
