using System.Collections;
using System.Collections.Generic;
using System;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

namespace ET
{
	[FriendClass(typeof(DlgAccountLogin))]
	public static  class DlgAccountLoginSystem
	{

		public static void RegisterUIEvent(this DlgAccountLogin self)
		{
		 self.View.E_LoginButton.AddListenerAsync(self.OnLoginButtonClickHandler);
		}

		public static void ShowWindow(this DlgAccountLogin self, Entity contextData = null)
		{
			string[] lines = File.ReadAllLines(@"..\Excel\ServerAddress.txt");
			self.View.E_ServerAddressDropdown.options.Clear();

			foreach (var address in lines)
			{
				Dropdown.OptionData optionData = new Dropdown.OptionData();
				optionData.text = address;
				self.View.E_ServerAddressDropdown.options.Add(optionData);
			}

			self.View.E_AccountTMP_InputField.text = PlayerPrefs.GetString("Account",string.Empty);
			self.View.E_PasswordTMP_InputField.text = PlayerPrefs.GetString("Password",string.Empty);
		}

		public static async ETTask OnLoginButtonClickHandler(this DlgAccountLogin self)
		{
			string account = self.View.E_AccountTMP_InputField.text;
			string password = self.View.E_PasswordTMP_InputField.text;
			try
			{
				string address = self.View.E_ServerAddressDropdown.options[self.View.E_ServerAddressDropdown.value].text;
				await LoginHelper.Login(self.ZoneScene(), address, account, password);
				PlayerPrefs.SetString("Account", account);
				PlayerPrefs.SetString("Password",password);
			}
			catch (Exception e)
			{
				Log.Error(e.ToString());
			}
			
			await ETTask.CompletedTask;
		}


	}
}
