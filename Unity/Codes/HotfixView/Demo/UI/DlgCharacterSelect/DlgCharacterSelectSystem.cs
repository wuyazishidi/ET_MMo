using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace ET
{
    [FriendClass(typeof (DlgCharacterSelect))]
    [FriendClass(typeof (RoleInfosComponent))]
    [FriendClass(typeof (RoleInfo))]
    public static class DlgCharacterSelectSystem
    {
        public static void RegisterUIEvent(this DlgCharacterSelect self)
        {
            self.View.E_CharacterListLoopHorizontalScrollRect.AddItemRefreshListener(self.OnLoopRefreshHandler);
            self.View.E_EnterGameButton.AddListenerAsync(self.OnEnterGameHandler);
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
        
        public static async ETTask OnEnterGameHandler(this DlgCharacterSelect self)
        {
            try
            {
                int errCode = await LoginHelper.EnterMap(self.ZoneScene());
                if (errCode != ErrorCode.ERR_Success)
                {
                    return;
                }
            }
            catch (Exception e)
            {
               Log.Error(e.ToString());
            }
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
                scrollItemCharacter.E_DeleteRoleButton.AddListenerAsync(() => { return self.DeleteRoleClickHandler(roleInfo.Id); });

                scrollItemCharacter.E_TouchButton.AddListenerWithId(self.OnTouchClickHandler, roleInfo.Id);
                scrollItemCharacter.E_SelectedFrameImage.SetVisible(roleInfo.Id == self.ZoneScene().GetComponent<RoleInfosComponent>().CurrentUnitId);
            }
        }

        public static void OnTouchClickHandler(this DlgCharacterSelect self, long unitId)
        {
            self.ZoneScene().GetComponent<RoleInfosComponent>().CurrentUnitId = unitId;
            self.View.E_CharacterListLoopHorizontalScrollRect.RefreshCells();
        }

        public static void OnAddRoleClickHandler(this DlgCharacterSelect self)
        {
            self.DomainScene().GetComponent<UIComponent>().HideWindow(WindowID.WindowID_CharacterSelect);
            self.DomainScene().GetComponent<UIComponent>().ShowWindow(WindowID.WindowID_CreateCharacter);
        }

        public static async ETTask DeleteRoleClickHandler(this DlgCharacterSelect self, long roleId)
        {
            try
            {
                int errCode = await LoginHelper.DeleteRole(self.ZoneScene(), roleId);
                if (errCode != ErrorCode.ERR_Success)
                {
                    return;
                }

                self.View.E_CharacterListLoopHorizontalScrollRect.RefreshCells();
            }
            catch (Exception e)
            {
                Log.Error(e.ToString());
            }
        }
    }
}