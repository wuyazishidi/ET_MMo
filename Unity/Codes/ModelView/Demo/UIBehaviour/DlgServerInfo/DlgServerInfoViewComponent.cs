
using UnityEngine;
using UnityEngine.UI;
namespace ET
{
	[ComponentOf(typeof(UIBaseWindow))]
	[EnableMethod]
	public  class DlgServerInfoViewComponent : Entity,IAwake,IDestroy 
	{
		public UnityEngine.UI.LoopVerticalScrollRect E_ServerListLoopVerticalScrollRect
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_E_ServerListLoopVerticalScrollRect == null )
     			{
		    		this.m_E_ServerListLoopVerticalScrollRect = UIFindHelper.FindDeepChild<UnityEngine.UI.LoopVerticalScrollRect>(this.uiTransform.gameObject,"BackGround/E_ServerList");
     			}
     			return this.m_E_ServerListLoopVerticalScrollRect;
     		}
     	}

		public void DestroyWidget()
		{
			this.m_E_ServerListLoopVerticalScrollRect = null;
			this.uiTransform = null;
		}

		private UnityEngine.UI.LoopVerticalScrollRect m_E_ServerListLoopVerticalScrollRect = null;
		public Transform uiTransform = null;
	}
}
