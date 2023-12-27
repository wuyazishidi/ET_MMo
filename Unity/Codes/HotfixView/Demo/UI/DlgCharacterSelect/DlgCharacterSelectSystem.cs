using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace ET
{
	[FriendClass(typeof(DlgCharacterSelect))]
	[FriendClass(typeof(RoleInfo))]
	public static  class DlgCharacterSelectSystem
	{

		public static void RegisterUIEvent(this DlgCharacterSelect self)
		{
		 self.View.E_CharacterListLoopHorizontalScrollRect.AddItemRefreshListener(self.OnLoopRefreshHandler);
		}

		public static void ShowWindow(this DlgCharacterSelect self, Entity contextData = null)
		{
			self.AddUIScrollItems(ref self.ScrollItemCharacters, 4);
			self.View.E_CharacterListLoopHorizontalScrollRect.SetVisible(true, 4);
			
		}

		public static void HideWindow(this DlgCharacterSelect self)
		{
			self.RemoveUIScrollItems(ref self.ScrollItemCharacters);
		}

		public static void OnLoopRefreshHandler(this DlgCharacterSelect self, Transform transform, int index)
		{
			Scroll_Item_character scrollItemCharacter = self.ScrollItemCharacters[index].BindTrans(transform);
			RoleInfo roleInfo = self.ZoneScene().GetComponent<RoleInfosComponent>().GetRoleInfoByIndex(index);
			if (roleInfo == null)
			{
				scrollItemCharacter.EG_AddRoleRectTransform.SetVisible(true);
				scrollItemCharacter.EG_RoleRectTransform.SetVisible(false);
				scrollItemCharacter.E_SelectedFrameImage.SetVisible(false);
				scrollItemCharacter.E_AddRoleButton.AddListener(self.OnAddRoleClickHandler);
			}
			else
			{
				scrollItemCharacter.EG_RoleRectTransform.SetVisible(true);
				scrollItemCharacter.EG_AddRoleRectTransform.SetVisible(false);
				scrollItemCharacter.E_RoleLevelTextMeshProUGUI.text = $"Lv.{roleInfo.Level}";
				scrollItemCharacter.E_RoleNameTextMeshProUGUI.text = roleInfo.Name;
			}
		}

		public static void OnAddRoleClickHandler(this DlgCharacterSelect self)
		{
			self.ZoneScene().GetComponent<UIComponent>().HideWindow(WindowID.WindowID_CharacterSelect);
			self.ZoneScene().GetComponent<UIComponent>().ShowWindow(WindowID.WindowID_CreateCharacter);
		}
	}
}
