
using UnityEngine;
using UnityEngine.UI;
namespace ET
{
	[EnableMethod]
	public  class Scroll_Item_character : Entity,IAwake,IDestroy,IUIScrollItem 
	{
		public long DataId {get;set;}
		private bool isCacheNode = false;
		public void SetCacheMode(bool isCache)
		{
			this.isCacheNode = isCache;
		}

		public Scroll_Item_character BindTrans(Transform trans)
		{
			this.uiTransform = trans;
			return this;
		}

		public UnityEngine.RectTransform EG_AddRoleRectTransform
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
     				if( this.m_EG_AddRoleRectTransform == null )
     				{
		    			this.m_EG_AddRoleRectTransform = UIFindHelper.FindDeepChild<UnityEngine.RectTransform>(this.uiTransform.gameObject,"EG_AddRole");
     				}
     				return this.m_EG_AddRoleRectTransform;
     			}
     			else
     			{
		    		return UIFindHelper.FindDeepChild<UnityEngine.RectTransform>(this.uiTransform.gameObject,"EG_AddRole");
     			}
     		}
     	}

		public UnityEngine.UI.Button E_AddRoleButton
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
     				if( this.m_E_AddRoleButton == null )
     				{
		    			this.m_E_AddRoleButton = UIFindHelper.FindDeepChild<UnityEngine.UI.Button>(this.uiTransform.gameObject,"EG_AddRole/E_AddRole");
     				}
     				return this.m_E_AddRoleButton;
     			}
     			else
     			{
		    		return UIFindHelper.FindDeepChild<UnityEngine.UI.Button>(this.uiTransform.gameObject,"EG_AddRole/E_AddRole");
     			}
     		}
     	}

		public UnityEngine.UI.Image E_AddRoleImage
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
     				if( this.m_E_AddRoleImage == null )
     				{
		    			this.m_E_AddRoleImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject,"EG_AddRole/E_AddRole");
     				}
     				return this.m_E_AddRoleImage;
     			}
     			else
     			{
		    		return UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject,"EG_AddRole/E_AddRole");
     			}
     		}
     	}

		public UnityEngine.RectTransform EG_RoleRectTransform
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
     				if( this.m_EG_RoleRectTransform == null )
     				{
		    			this.m_EG_RoleRectTransform = UIFindHelper.FindDeepChild<UnityEngine.RectTransform>(this.uiTransform.gameObject,"EG_Role");
     				}
     				return this.m_EG_RoleRectTransform;
     			}
     			else
     			{
		    		return UIFindHelper.FindDeepChild<UnityEngine.RectTransform>(this.uiTransform.gameObject,"EG_Role");
     			}
     		}
     	}

		public UnityEngine.UI.Button E_TouchButton
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
     				if( this.m_E_TouchButton == null )
     				{
		    			this.m_E_TouchButton = UIFindHelper.FindDeepChild<UnityEngine.UI.Button>(this.uiTransform.gameObject,"EG_Role/E_Touch");
     				}
     				return this.m_E_TouchButton;
     			}
     			else
     			{
		    		return UIFindHelper.FindDeepChild<UnityEngine.UI.Button>(this.uiTransform.gameObject,"EG_Role/E_Touch");
     			}
     		}
     	}

		public UnityEngine.UI.Image E_TouchImage
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
     				if( this.m_E_TouchImage == null )
     				{
		    			this.m_E_TouchImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject,"EG_Role/E_Touch");
     				}
     				return this.m_E_TouchImage;
     			}
     			else
     			{
		    		return UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject,"EG_Role/E_Touch");
     			}
     		}
     	}

		public UnityEngine.UI.Button E_DeleteRoleButton
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
     				if( this.m_E_DeleteRoleButton == null )
     				{
		    			this.m_E_DeleteRoleButton = UIFindHelper.FindDeepChild<UnityEngine.UI.Button>(this.uiTransform.gameObject,"EG_Role/CharacterSelectInfoTop/E_DeleteRole");
     				}
     				return this.m_E_DeleteRoleButton;
     			}
     			else
     			{
		    		return UIFindHelper.FindDeepChild<UnityEngine.UI.Button>(this.uiTransform.gameObject,"EG_Role/CharacterSelectInfoTop/E_DeleteRole");
     			}
     		}
     	}

		public UnityEngine.UI.Image E_DeleteRoleImage
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
     				if( this.m_E_DeleteRoleImage == null )
     				{
		    			this.m_E_DeleteRoleImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject,"EG_Role/CharacterSelectInfoTop/E_DeleteRole");
     				}
     				return this.m_E_DeleteRoleImage;
     			}
     			else
     			{
		    		return UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject,"EG_Role/CharacterSelectInfoTop/E_DeleteRole");
     			}
     		}
     	}

		public TMPro.TextMeshProUGUI E_RoleLevelTextMeshProUGUI
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
     				if( this.m_E_RoleLevelTextMeshProUGUI == null )
     				{
		    			this.m_E_RoleLevelTextMeshProUGUI = UIFindHelper.FindDeepChild<TMPro.TextMeshProUGUI>(this.uiTransform.gameObject,"EG_Role/CaaracterInfo/E_RoleLevel");
     				}
     				return this.m_E_RoleLevelTextMeshProUGUI;
     			}
     			else
     			{
		    		return UIFindHelper.FindDeepChild<TMPro.TextMeshProUGUI>(this.uiTransform.gameObject,"EG_Role/CaaracterInfo/E_RoleLevel");
     			}
     		}
     	}

		

		public TMPro.TextMeshProUGUI E_RoleNameTextMeshProUGUI
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
     				if( this.m_E_RoleNameTextMeshProUGUI == null )
     				{
		    			this.m_E_RoleNameTextMeshProUGUI = UIFindHelper.FindDeepChild<TMPro.TextMeshProUGUI>(this.uiTransform.gameObject,"EG_Role/CaaracterInfo/E_RoleName");
     				}
     				return this.m_E_RoleNameTextMeshProUGUI;
     			}
     			else
     			{
		    		return UIFindHelper.FindDeepChild<TMPro.TextMeshProUGUI>(this.uiTransform.gameObject,"EG_Role/CaaracterInfo/E_RoleName");
     			}
     		}
     	}

		

		public UnityEngine.UI.Image E_SelectedFrameImage
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
     				if( this.m_E_SelectedFrameImage == null )
     				{
		    			this.m_E_SelectedFrameImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject,"Frame/E_SelectedFrame");
     				}
     				return this.m_E_SelectedFrameImage;
     			}
     			else
     			{
		    		return UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject,"Frame/E_SelectedFrame");
     			}
     		}
     	}

		public void DestroyWidget()
		{
			this.m_EG_AddRoleRectTransform = null;
			this.m_E_AddRoleButton = null;
			this.m_E_AddRoleImage = null;
			this.m_EG_RoleRectTransform = null;
			this.m_E_TouchButton = null;
			this.m_E_TouchImage = null;
			this.m_E_DeleteRoleButton = null;
			this.m_E_DeleteRoleImage = null;
			this.m_E_RoleLevelTextMeshProUGUI = null;
			this.m_E_RoleLevelTextMeshProUGUI = null;
			this.m_E_RoleNameTextMeshProUGUI = null;
			this.m_E_RoleNameTextMeshProUGUI = null;
			this.m_E_SelectedFrameImage = null;
			this.uiTransform = null;
			this.DataId = 0;
		}

		private UnityEngine.RectTransform m_EG_AddRoleRectTransform = null;
		private UnityEngine.UI.Button m_E_AddRoleButton = null;
		private UnityEngine.UI.Image m_E_AddRoleImage = null;
		private UnityEngine.RectTransform m_EG_RoleRectTransform = null;
		private UnityEngine.UI.Button m_E_TouchButton = null;
		private UnityEngine.UI.Image m_E_TouchImage = null;
		private UnityEngine.UI.Button m_E_DeleteRoleButton = null;
		private UnityEngine.UI.Image m_E_DeleteRoleImage = null;
		private TMPro.TextMeshProUGUI m_E_RoleLevelTextMeshProUGUI = null;
		private TMPro.TextMeshProUGUI m_E_RoleNameTextMeshProUGUI = null;
		private UnityEngine.UI.Image m_E_SelectedFrameImage = null;
		public Transform uiTransform = null;
	}
}
