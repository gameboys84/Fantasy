
using Unity.VisualScripting;
using UnityEngine;

// UIManager是单例模式的管理类，负责管理UI的显示和隐藏，以及UI的交互逻辑
[Singleton]
public class UIManager : MonoBehaviour, ISingleton
{
    [SerializeField] GameObject uiLogin;
    [SerializeField] GameObject uiLobby;
    
    public void ShowLoginUI()
    {
        uiLogin.SetActive(true);
        uiLobby.SetActive(false);
    }

    public void ShowLobbyUI()
    {
        uiLogin.SetActive(false);
        uiLobby.SetActive(true);
    }
}
