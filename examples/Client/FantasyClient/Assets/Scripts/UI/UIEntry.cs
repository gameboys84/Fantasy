using Fantasy;
using UnityEngine;
using UnityEngine.UI;

namespace GameLogic
{
	class UIEntry : UIBase
	{
		#region 脚本工具生成的代码
		private Button m_btnConnentServerButton;
		private InputField m_inputIPPort;
		private Button m_btnSendButton;
		private Button m_btnSendRPCButton;
		private Button m_btnReceiveButton;
		private Button m_btnLoginAddressButton;
		private Button m_btnSendAddressButton;
		private Button m_btnSendAddressRPCButton;
		private Button m_btnReceiveAddressButton;
		private Button m_btnLoginUIButton;
		private Text m_textMessage;
		
		public override void ScriptGenerator()
		{
			m_btnConnentServerButton = FindChildComponent<Button>("Scroll View/Viewport/UIEntry/m_btnConnentServerButton");
			m_inputIPPort = FindChildComponent<InputField>("Scroll View/Viewport/UIEntry/m_inputIPPort");
			m_btnSendButton = FindChildComponent<Button>("Scroll View/Viewport/UIEntry/m_btnSendButton");
			m_btnSendRPCButton = FindChildComponent<Button>("Scroll View/Viewport/UIEntry/m_btnSendRPCButton");
			m_btnReceiveButton = FindChildComponent<Button>("Scroll View/Viewport/UIEntry/m_btnReceiveButton");
			m_btnLoginAddressButton = FindChildComponent<Button>("Scroll View/Viewport/UIEntry/m_btnLoginAddressButton");
			m_btnSendAddressButton = FindChildComponent<Button>("Scroll View/Viewport/UIEntry/m_btnSendAddressButton");
			m_btnSendAddressRPCButton = FindChildComponent<Button>("Scroll View/Viewport/UIEntry/m_btnSendAddressRPCButton");
			m_btnReceiveAddressButton = FindChildComponent<Button>("Scroll View/Viewport/UIEntry/m_btnReceiveAddressButton");
			m_btnLoginUIButton = FindChildComponent<Button>("Scroll View/Viewport/UIEntry/m_btnLoginUIButton");
			m_textMessage = FindChildComponent<Text>("Scroll View/Viewport/UIEntry/Panel/m_textMessage");
			m_btnConnentServerButton.onClick.AddListener(OnClickConnentServerButtonBtn);
			m_btnSendButton.onClick.AddListener(OnClickSendButtonBtn);
			m_btnSendRPCButton.onClick.AddListener(OnClickSendRPCButtonBtn);
			m_btnReceiveButton.onClick.AddListener(OnClickReceiveButtonBtn);
			m_btnLoginAddressButton.onClick.AddListener(OnClickLoginAddressButtonBtn);
			m_btnSendAddressButton.onClick.AddListener(OnClickSendAddressButtonBtn);
			m_btnSendAddressRPCButton.onClick.AddListener(OnClickSendAddressRPCButtonBtn);
			m_btnReceiveAddressButton.onClick.AddListener(OnClickReceiveAddressButtonBtn);
			m_btnLoginUIButton.onClick.AddListener(OnClickLoginUIButtonBtn);
		}
		#endregion

		#region 事件
		private void OnClickSendButtonBtn()
		{
			Log("OnClickSendButtonBtn");
			NetworkManager.Instance.Session.Send(new C2G_TestMessage()
			{
				Tag = "OnClickSendButtonBtn"
			});
		}
		private async void OnClickSendRPCButtonBtn()
		{
			Log("OnClickSendRPCButtonBtn");
			var rsp = await NetworkManager.Instance.Session.Call(new C2G_TestRequest()
			{
				Tag = "OnClickSendRPCButtonBtn"
			});

			if (rsp.ErrorCode != 0)
			{
				Log("OnClickSendRPCButtonBtn ErrorCode:" + rsp.ErrorCode);
				return;
			}
			
			var content = (G2C_TestResponse)rsp;
			Log("OnClickSendRPCButtonBtn content:" + content.Tag);
		}
		private void OnClickReceiveButtonBtn()
		{
			Log("OnClickReceiveButtonBtn, 通知类消息要求服务器先缓存玩家session，再在需要时定向发送通知，主要的实现在服务端，这里就不演示了");
			// NetworkManager.Instance.Session.Send(new C2G_TestNotifyMessage()
			// {
			// 	Msg = "OnClickReceiveButtonBtn"
			// });
		}
		
		private async void OnClickLoginAddressButtonBtn()
		{
			Log("OnClickLoginAddressButtonBtn");
			var rsp = await NetworkManager.Instance.Session.Call(new C2G_CreateAddressableRequest()
			{
				
			});

			if (rsp.ErrorCode != 0)
			{
				Log("OnClickLoginAddressButtonBtn ErrorCode:" + rsp.ErrorCode);
				AddressRegisted = false;
				return;
			}
			
			var content = (G2C_CreateAddressableResponse)rsp;
			Log("OnClickLoginAddressButtonBtn OK");

			AddressRegisted = true;
		}
		private void OnClickSendAddressButtonBtn()
		{
			Log("OnClickSendAddressButtonBtn");
			NetworkManager.Instance.Session.Send(new C2M_TestMessage()
			{
				Tag = "OnClickSendAddressButtonBtn"
			});
		}
		private async void OnClickSendAddressRPCButtonBtn()
		{
			Log("OnClickSendAddressRPCButtonBtn");
			var rsp = await NetworkManager.Instance.Session.Call(new C2M_TestRequest()
			{
				Tag = "OnClickSendAddressRPCButtonBtn"
			});

			if (rsp.ErrorCode != 0)
			{
				Log($"OnClickSendAddressRPCButtonBtn: {rsp.ErrorCode}");
				return;
			}

			var content = (M2C_TestResponse)rsp;
			Log("OnClickSendAddressRPCButtonBtn content:" + content.Tag);
		}
		private void OnClickReceiveAddressButtonBtn()
		{
			Log("OnClickReceiveAddressButtonBtn, 通知类消息要求服务器先缓存玩家session，再在需要时定向发送通知，主要的实现在服务端，这里就不演示了");
			// NetworkManager.Instance.Session.Send(new C2M_TestNotifyAddressableMessage()
			// {
			// 	Msg = "OnClickReceiveAddressButtonBtn"
			// });
		}
		private void OnClickConnentServerButtonBtn()
		{
			string ip = "127.0.0.1";
			int port = 20000;
			
			string ipPort = m_inputIPPort.text;
			if (!string.IsNullOrEmpty(ipPort))
			{
				string[] ipPortArr = ipPort.Split(':');
				if (ipPortArr.Length == 2)
				{
					ip = ipPortArr[0];
					if (!int.TryParse(ipPortArr[1], out port))
					{
						port = 20000;
					}
				}
				else if (ipPortArr.Length == 1)
				{
					ip = ipPortArr[0];
				}
			}
			Log("OnClickConnentServerButtonBtn ip:" + ip + " port:" + port);
			try
			{
				NetworkManager.Instance.StartConnect(ip, port);
			}
			catch (System.Exception e)
			{
				Log($"ERROR: Connect {ip}:{port}: {e}");
			}
		}
		private void OnClickLoginUIButtonBtn()
		{
			NetworkManager.Instance.Disconnect();
		}
		#endregion

		private bool AddressRegisted = false;
		
		public void Log(string message)
		{
			m_textMessage.text = message;
			// var tf = m_textMessage.GetComponent<RectTransform>();
			// var tfParent = m_textMessage.transform.parent.GetComponent<RectTransform>();
			// tfParent.offsetMin = Vector2.zero;
			// tfParent.offsetMax = new Vector2(-5, Screen.height - tf.sizeDelta.y);
			// tfParent.sizeDelta = new Vector2(0, tf.sizeDelta.y);
			// tfParent.anchoredPosition = new Vector2(0, tf.sizeDelta.y / 2);
			Debug.Log(message);
		}

		private void Update()
		{
			var state = NetworkManager.Instance.GetConnectState();
			bool isConnect = state == NetworkManager.ConnectionState.CONNECTED;
			bool isAddressed = isConnect && AddressRegisted;
			m_btnConnentServerButton.interactable = (state == NetworkManager.ConnectionState.NOT_INITED || state == NetworkManager.ConnectionState.NOT_CONNECTED);
			m_btnSendButton.interactable = isConnect;
			m_btnSendRPCButton.interactable = isConnect;
			m_btnReceiveButton.interactable = isConnect;
			m_btnLoginAddressButton.interactable = isConnect && !AddressRegisted;
			m_btnSendAddressButton.interactable = isAddressed;
			m_btnSendAddressRPCButton.interactable = isAddressed;
			m_btnReceiveAddressButton.interactable = isAddressed;
			m_btnLoginUIButton.interactable = !m_btnConnentServerButton.interactable; // (state == NetworkManager.ConnectionState.CONNECTING || state == NetworkManager.ConnectionState.CONNECTED);
		}
	}
}
