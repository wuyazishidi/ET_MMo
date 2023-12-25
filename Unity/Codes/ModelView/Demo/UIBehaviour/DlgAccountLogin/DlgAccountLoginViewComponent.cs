
using UnityEngine;
using UnityEngine.UI;
namespace ET
{
	[ComponentOf(typeof(UIBaseWindow))]
	[EnableMethod]
	public  class DlgAccountLoginViewComponent : Entity,IAwake,IDestroy 
	{
		public UnityEngine.UI.Button E_CloseButton
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_E_CloseButton == null )
     			{
		    		this.m_E_CloseButton = UIFindHelper.FindDeepChild<UnityEngine.UI.Button>(this.uiTransform.gameObject,"Popup_Login/Popup/E_Close");
     			}
     			return this.m_E_CloseButton;
     		}
     	}

		public UnityEngine.UI.Image E_CloseImage
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_E_CloseImage == null )
     			{
		    		this.m_E_CloseImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject,"Popup_Login/Popup/E_Close");
     			}
     			return this.m_E_CloseImage;
     		}
     	}

		public UnityEngine.UI.Button E_LoginButton
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_E_LoginButton == null )
     			{
		    		this.m_E_LoginButton = UIFindHelper.FindDeepChild<UnityEngine.UI.Button>(this.uiTransform.gameObject,"Popup_Login/Popup/E_Login");
     			}
     			return this.m_E_LoginButton;
     		}
     	}

		public UnityEngine.UI.Image E_LoginImage
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_E_LoginImage == null )
     			{
		    		this.m_E_LoginImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject,"Popup_Login/Popup/E_Login");
     			}
     			return this.m_E_LoginImage;
     		}
     	}

		public UnityEngine.UI.Image E_AccountImage
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_E_AccountImage == null )
     			{
		    		this.m_E_AccountImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject,"Popup_Login/Popup/InputFields/E_Account");
     			}
     			return this.m_E_AccountImage;
     		}
     	}

		public TMPro.TMP_InputField E_AccountTMP_InputField
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_E_AccountTMP_InputField == null )
     			{
		    		this.m_E_AccountTMP_InputField = UIFindHelper.FindDeepChild<TMPro.TMP_InputField>(this.uiTransform.gameObject,"Popup_Login/Popup/InputFields/E_Account");
     			}
     			return this.m_E_AccountTMP_InputField;
     		}
     	}

		public UnityEngine.UI.Image E_PasswordImage
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_E_PasswordImage == null )
     			{
		    		this.m_E_PasswordImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject,"Popup_Login/Popup/InputFields/E_Password");
     			}
     			return this.m_E_PasswordImage;
     		}
     	}

		public TMPro.TMP_InputField E_PasswordTMP_InputField
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_E_PasswordTMP_InputField == null )
     			{
		    		this.m_E_PasswordTMP_InputField = UIFindHelper.FindDeepChild<TMPro.TMP_InputField>(this.uiTransform.gameObject,"Popup_Login/Popup/InputFields/E_Password");
     			}
     			return this.m_E_PasswordTMP_InputField;
     		}
     	}

		public UnityEngine.UI.Dropdown E_ServerAddressDropdown
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_E_ServerAddressDropdown == null )
     			{
		    		this.m_E_ServerAddressDropdown = UIFindHelper.FindDeepChild<UnityEngine.UI.Dropdown>(this.uiTransform.gameObject,"Popup_Login/Popup/E_ServerAddress");
     			}
     			return this.m_E_ServerAddressDropdown;
     		}
     	}

		public UnityEngine.UI.Image E_ServerAddressImage
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_E_ServerAddressImage == null )
     			{
		    		this.m_E_ServerAddressImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject,"Popup_Login/Popup/E_ServerAddress");
     			}
     			return this.m_E_ServerAddressImage;
     		}
     	}

		public void DestroyWidget()
		{
			this.m_E_CloseButton = null;
			this.m_E_CloseImage = null;
			this.m_E_LoginButton = null;
			this.m_E_LoginImage = null;
			this.m_E_AccountImage = null;
			this.m_E_AccountTMP_InputField = null;
			this.m_E_PasswordImage = null;
			this.m_E_PasswordTMP_InputField = null;
			this.m_E_ServerAddressDropdown = null;
			this.m_E_ServerAddressImage = null;
			this.uiTransform = null;
		}

		private UnityEngine.UI.Button m_E_CloseButton = null;
		private UnityEngine.UI.Image m_E_CloseImage = null;
		private UnityEngine.UI.Button m_E_LoginButton = null;
		private UnityEngine.UI.Image m_E_LoginImage = null;
		private UnityEngine.UI.Image m_E_AccountImage = null;
		private TMPro.TMP_InputField m_E_AccountTMP_InputField = null;
		private UnityEngine.UI.Image m_E_PasswordImage = null;
		private TMPro.TMP_InputField m_E_PasswordTMP_InputField = null;
		private UnityEngine.UI.Dropdown m_E_ServerAddressDropdown = null;
		private UnityEngine.UI.Image m_E_ServerAddressImage = null;
		public Transform uiTransform = null;
	}
}
