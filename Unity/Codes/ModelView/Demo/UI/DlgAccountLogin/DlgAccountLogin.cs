namespace ET
{
	 [ComponentOf(typeof(UIBaseWindow))]
	public  class DlgAccountLogin :Entity,IAwake,IUILogic
	{

		public DlgAccountLoginViewComponent View { get => this.Parent.GetComponent<DlgAccountLoginViewComponent>();} 

		 

	}
}
