namespace ET
{
	 [ComponentOf(typeof(UIBaseWindow))]
	public  class DlgQueue :Entity,IAwake,IUILogic
	{

		public DlgQueueViewComponent View { get => this.Parent.GetComponent<DlgQueueViewComponent>();} 

		 

	}
}
