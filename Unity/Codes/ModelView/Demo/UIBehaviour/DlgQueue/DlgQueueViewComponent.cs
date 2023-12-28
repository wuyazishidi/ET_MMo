
using UnityEngine;
using UnityEngine.UI;
namespace ET
{
	[ComponentOf(typeof(UIBaseWindow))]
	[EnableMethod]
	public  class DlgQueueViewComponent : Entity,IAwake,IDestroy 
	{
		public TMPro.TextMeshProUGUI E_lineTipTextMeshProUGUI
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_E_lineTipTextMeshProUGUI == null )
     			{
		    		this.m_E_lineTipTextMeshProUGUI = UIFindHelper.FindDeepChild<TMPro.TextMeshProUGUI>(this.uiTransform.gameObject,"Popup_Name/Popup/E_lineTip");
     			}
     			return this.m_E_lineTipTextMeshProUGUI;
     		}
     	}

		

		public UnityEngine.UI.Button E_ExitButton
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_E_ExitButton == null )
     			{
		    		this.m_E_ExitButton = UIFindHelper.FindDeepChild<UnityEngine.UI.Button>(this.uiTransform.gameObject,"Popup_Name/Popup/E_Exit");
     			}
     			return this.m_E_ExitButton;
     		}
     	}

		public UnityEngine.UI.Image E_ExitImage
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_E_ExitImage == null )
     			{
		    		this.m_E_ExitImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject,"Popup_Name/Popup/E_Exit");
     			}
     			return this.m_E_ExitImage;
     		}
     	}

		public void DestroyWidget()
		{
			this.m_E_lineTipTextMeshProUGUI = null;
			this.m_E_ExitButton = null;
			this.m_E_ExitImage = null;
			this.uiTransform = null;
		}

		private TMPro.TextMeshProUGUI m_E_lineTipTextMeshProUGUI = null;
		private UnityEngine.UI.Button m_E_ExitButton = null;
		private UnityEngine.UI.Image m_E_ExitImage = null;
		public Transform uiTransform = null;
	}
}
