using Fantasy;
using Fantasy.Async;
using Fantasy.Network;
using Fantasy.Platform.Unity;
using GameLogic;
using UnityEngine;

public class NetworkManager : MonoBehaviour
{
    public enum ConnectionState
    {
        NOT_INITED = -1,
        NOT_CONNECTED = 0,
        CONNECTING = 1,
        CONNECTED = 2,
    }
    
    public static NetworkManager Instance => _instance;
    public Session Session => _session;
    private bool _isConnected = false;
    private string ip = "127.0.0.1";
    private int port = 20000;
    private int timeout = 5000;
    
    private static NetworkManager _instance;
    private Fantasy.Scene _scene;
    private Session _session;
    private bool _inited;
    
    private bool _shouldReconnect = true;
    private int _reconnectCount = 0;
    private bool _isReconnecting = false;

    private void Awake()
    {
        _instance = this;
        DontDestroyOnLoad(gameObject);
    }

    private void Update()
    {
        var state = GetConnectState();
        // Debug.Log($"Update: {(_session != null ? _session is { IsDisposed: false } : "null")}, state:{state}");

        if (state == ConnectionState.NOT_CONNECTED)
        {
            if (_shouldReconnect && !_isReconnecting)
            {
                Reconnect();
            }
        }
    }

    public async FTask Initialize()
    {
        if (_inited)
            return;
        
        _isConnected = false;
        
        await Entry.Initialize(GetType().Assembly);
        _inited = true;
        _reconnectCount = 0;
        
        _scene = await Scene.Create(SceneRuntimeMode.MainThread);
    }

    public void StartConnect(string _ip, int _port, int _timeout = 5000)
    {
        this.ip = _ip;
        this.port = _port;
        this.timeout = _timeout;
        
        var state = GetConnectState();
        if (state == ConnectionState.NOT_INITED || state == ConnectionState.NOT_CONNECTED)
        {
            _shouldReconnect = true;
            _reconnectCount = 0;
            Connect();
        }
        else if (state == ConnectionState.CONNECTED || state == ConnectionState.CONNECTING)
        {
            // 已经连接, 或正在连接中，不需要再次连接
            Utils.Log($"state: {state}, 当前不能连接");
            return;
        }
    }

    public void Disconnect()
    {
        if (_session is { IsDisposed: false })
        {
            _session.Dispose();
            _session = null;
        }
    }
    
    private void Connect()
    {
        _session = _scene.Connect(
            $"{ip}:{port}",
            NetworkProtocolType.KCP,
            OnConnectComplete,
            OnConnectFail,
            OnConnectDisconnect,
            false, timeout);
    }

    private void OnConnectFail()
    {
        Utils.Log("连接失败 Fail", Color.red);
        OnDisconnected();
    }

    private void OnConnectDisconnect()
    {
        Utils.Log($"连接断开 Disconnect, {_reconnectCount}, {_shouldReconnect}", Color.red);
        OnDisconnected();
    }

    private void OnDisconnected()
    {
        _isConnected = false;
        _isReconnecting = false;
    }

    private void Reconnect()
    {
        ++_reconnectCount;
        if (_reconnectCount > 3)
        {
            _shouldReconnect = false;
            Utils.Log($"尝试重新连接次数过多:{_reconnectCount}，放弃连接", Color.red);
            return;
        }
        
        _isReconnecting = true;
        Utils.Log($"尝试重新连接... {_reconnectCount}", Color.yellow);

        // 延迟几秒再尝试连接
        _scene.TimerComponent.Net.OnceTimer(1000, () =>
            NetworkManager.Instance.Connect());
    }

    private void OnConnectComplete()
    {
        Utils.Log("连接成功", Color.green);
        _reconnectCount = 0;
        _isConnected = true;
        
        // 每interval 2秒向服务器发送一次心跳，用于向服务器保活
        // 本地每 timeOutInterval 3秒检测 上次服务器回应是否超时， 超时时间为 timeOut 2秒
        _session.AddComponent<SessionHeartbeatComponent>().Start(2000);
    }

    /// <summary>
    /// 获取连接状态
    /// </summary>
    /// <returns>0: 未连接, 1: 连接中, 2: 已连接</returns>
    public ConnectionState GetConnectState()
    {
        if (_session == null)
        {
            return ConnectionState.NOT_INITED;
        }

        if (_session.IsDisposed == false)
        {
            // session已经建立没释放, 表示可能已经连接成功或正在连接中
            return _isConnected ? ConnectionState.CONNECTED : ConnectionState.CONNECTING;
        }
        else
        {
            // 重连中也算在连接
            return _isReconnecting ? ConnectionState.CONNECTING : ConnectionState.NOT_CONNECTED;
        }
    }
}
