namespace ET
{
	 [ComponentOf(typeof(UIBaseWindow))]
	public  class DlgTapToStart :Entity,IAwake,IUILogic
	{

		public DlgTapToStartViewComponent View { get => this.Parent.GetComponent<DlgTapToStartViewComponent>();} 

		 

	}
}
