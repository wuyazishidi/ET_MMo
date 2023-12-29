using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace ET
{
	[FriendClass(typeof(DlgQueue))]
	public static  class DlgQueueSystem
	{

		public static void RegisterUIEvent(this DlgQueue self)
		{
		 self.View.E_ExitButton.AddListenerAsync(self.OnCancelQueueHandler);
		}

		public static void ShowWindow(this DlgQueue self, Entity contextData = null)
		{
		}

		public static void Refresh(this DlgQueue self,int index,int count)
		{
			self.View.E_lineTipTextMeshProUGUI.text =
					string.Format("当前共有<color=yellow>{0}人</color>正在排队进入服务器\n您正位于<color=yellow>第{1}位</color>", count, index);
		}

		public static async ETTask OnCancelQueueHandler(this DlgQueue self)
		{
			try
			{
				int errCode = await LoginHelper.CancelQueue(self.ZoneScene());
				if (errCode != ErrorCode.ERR_Success)
				{
					return;
				}
				self.DomainScene().GetComponent<UIComponent>().HideWindow(WindowID.WindowID_Queue);
			}
			catch (Exception e)
			{
			    Log.Error(e.ToString());
			}
		}

	}
}
