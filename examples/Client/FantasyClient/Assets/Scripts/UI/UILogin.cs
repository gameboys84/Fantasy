using UnityEngine;
using UnityEngine.UI;

namespace GameLogic
{
	class UILogin : UIBase
	{
		#region 脚本工具生成的代码
		private GameObject m_goLogin;
		private InputField m_inputPassWord;
		private InputField m_inputName;
		private Button m_btnLogin;
		public override void ScriptGenerator()
		{
			m_goLogin = FindChild("Panel/m_goLogin").gameObject;
			m_inputPassWord = FindChildComponent<InputField>("Panel/m_goLogin/m_inputPassWord");
			m_inputName = FindChildComponent<InputField>("Panel/m_goLogin/m_inputName");
			m_btnLogin = FindChildComponent<Button>("Panel/m_goLogin/m_btnLogin");
			m_btnLogin.onClick.AddListener(OnClickLoginBtn);
		}
		#endregion

		#region 事件
		private void OnClickLoginBtn()
		{
			Debug.Log("OnClickLoginBtn");
			
			// 获取账号和密码，然后发送登录请求
			string name = m_inputName.text;
			string password = m_inputPassWord.text;
			Debug.Log("name:" + name + " password:" + password);

			// 登录成功，关闭登录界面
			// UIManager.instance.CloseUI(UIType.UILogin);
		}
		#endregion

	}
}
