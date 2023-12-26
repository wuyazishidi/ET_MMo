using System.Collections;
using System.Collections.Generic;
using System;
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

		public static void OnLoopItemRefreshHandler(this DlgServerInfo self,Transform transform, int index)
		{
			Scroll_Item_serverInfo scrollItemServerInfo = self.ScrollItemServerInfos[index].BindTrans(transform);
			ServerInfo serverInfo = self.ZoneScene().GetComponent<ServerInfosComponent>().ServerInfosList[index];

			scrollItemServerInfo.E_ServerNameTextMeshProUGUI.text = serverInfo.Name;
			scrollItemServerInfo.E_ServerStatusImage.color = serverInfo.Status ==(int)ServerStatus.Active? Color.green : Color.gray;
		}

	}
}
