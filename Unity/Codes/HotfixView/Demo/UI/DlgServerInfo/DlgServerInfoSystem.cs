using System.Collections;
using System.Collections.Generic;
using System;
using CommandLine;
using UnityEngine;
using UnityEngine.UI;

namespace ET
{
	[FriendClass(typeof(DlgServerInfo))]
	[FriendClass(typeof(ServerInfosComponent))] 
	[FriendClass(typeof(ServerInfo))] 
	public static  class DlgServerInfoSystem
	{

		public static void RegisterUIEvent(this DlgServerInfo self)
		{
		 self.View.E_ServerListLoopVerticalScrollRect.AddItemRefreshListener(self.OnLoopItemRefreshHandler);
		}

		public static void ShowWindow(this DlgServerInfo self, Entity contextData = null)
		{
			int count = self.ZoneScene().GetComponent<ServerInfosComponent>().ServerInfosList.Count;
			self.AddUIScrollItems(ref self.ScrollItemServerInfos, count);
			self.View.E_ServerListLoopVerticalScrollRect.SetVisible(true,count);
		}
		
		public static void HideWindow(this DlgServerInfo self)
		{
			self.RemoveUIScrollItems(ref self.ScrollItemServerInfos);
		}

		public static void OnLoopItemRefreshHandler(this DlgServerInfo self,Transform transform, int index)
		{
			Scroll_Item_serverInfo scrollItemServerInfo = self.ScrollItemServerInfos[index].BindTrans(transform);
			ServerInfo serverInfo = self.ZoneScene().GetComponent<ServerInfosComponent>().ServerInfosList[index];

			scrollItemServerInfo.E_ServerNameTextMeshProUGUI.text = serverInfo.Name;
			scrollItemServerInfo.E_ServerStatusImage.color = serverInfo.Status ==(int)ServerStatus.Active? Color.green : Color.gray;
			scrollItemServerInfo.E_SelectButton.AddListenerAsyncWithId(self.OnSelectButtonHandler,serverInfo.ServerZone);
			
		}

		public static async ETTask OnSelectButtonHandler(this DlgServerInfo self,int zoneId)
		{
			try
			{
				int errcode = await LoginHelper.LoginZone(self.ZoneScene(), zoneId);
				if (errcode != ErrorCode.ERR_Success)
				{
					return;
				}

				self.DomainScene().GetComponent<UIComponent>().HideWindow(WindowID.WindowID_ServerInfo);
				self.DomainScene().GetComponent<UIComponent>().HideWindow(WindowID.WindowID_AccountLogin);
			}
			catch (Exception e)
			{
				Log.Error(e.ToString());
			}
		}
	}
}
