
using UnityEngine;
using UnityEngine.UI;
namespace ET
{
	[EnableMethod]
	public  class Scroll_Item_serverInfo : Entity,IAwake,IDestroy,IUIScrollItem 
	{
		public long DataId {get;set;}
		private bool isCacheNode = false;
		public void SetCacheMode(bool isCache)
		{
			this.isCacheNode = isCache;
		}

		public Scroll_Item_serverInfo BindTrans(Transform trans)
		{
			this.uiTransform = trans;
			return this;
		}

		public UnityEngine.UI.Button E_SelectButton
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if (this.isCacheNode)
     			{
     				if( this.m_E_SelectButton == null )
     				{
		    			this.m_E_SelectButton = UIFindHelper.FindDeepChild<UnityEngine.UI.Button>(this.uiTransform.gameObject,"E_Select");
     				}
     				return this.m_E_SelectButton;
     			}
     			else
     			{
		    		return UIFindHelper.FindDeepChild<UnityEngine.UI.Button>(this.uiTransform.gameObject,"E_Select");
     			}
     		}
     	}

		public UnityEngine.UI.Image E_SelectImage
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if (this.isCacheNode)
     			{
     				if( this.m_E_SelectImage == null )
     				{
		    			this.m_E_SelectImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject,"E_Select");
     				}
     				return this.m_E_SelectImage;
     			}
     			else
     			{
		    		return UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject,"E_Select");
     			}
     		}
     	}

		public UnityEngine.UI.Image E_ServerStatusImage
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if (this.isCacheNode)
     			{
     				if( this.m_E_ServerStatusImage == null )
     				{
		    			this.m_E_ServerStatusImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject,"E_ServerStatus");
     				}
     				return this.m_E_ServerStatusImage;
     			}
     			else
     			{
		    		return UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject,"E_ServerStatus");
     			}
     		}
     	}

		

		public TMPro.TextMeshProUGUI E_ServerNameTextMeshProUGUI
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if (this.isCacheNode)
     			{
     				if( this.m_E_ServerNameTextMeshProUGUI == null )
     				{
		    			this.m_E_ServerNameTextMeshProUGUI = UIFindHelper.FindDeepChild<TMPro.TextMeshProUGUI>(this.uiTransform.gameObject,"E_ServerName");
     				}
     				return this.m_E_ServerNameTextMeshProUGUI;
     			}
     			else
     			{
		    		return UIFindHelper.FindDeepChild<TMPro.TextMeshProUGUI>(this.uiTransform.gameObject,"E_ServerName");
     			}
     		}
     	}

		public void DestroyWidget()
		{
			this.m_E_SelectButton = null;
			this.m_E_SelectImage = null;
			this.m_E_ServerStatusImage = null;
			this.m_E_ServerNameTextMeshProUGUI = null;
			this.uiTransform = null;
			this.DataId = 0;
		}

		private UnityEngine.UI.Button m_E_SelectButton = null;
		private UnityEngine.UI.Image m_E_SelectImage = null;
		private UnityEngine.UI.Image m_E_ServerStatusImage = null;
		private TMPro.TextMeshProUGUI m_E_ServerNameTextMeshProUGUI = null;
		public Transform uiTransform = null;
	}
}
