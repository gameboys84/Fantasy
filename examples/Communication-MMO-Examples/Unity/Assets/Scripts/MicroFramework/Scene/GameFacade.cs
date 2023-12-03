using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

public class GameFacade : Singleton<GameFacade>
{
    private static PlayerUnits _playerUnit;
    public static PlayerUnits playerUnits => _playerUnit;
    private static MonsterUnits _MonsterUnit;
    public static MonsterUnits MonsterUnits => _MonsterUnit;
    private static NpcUnits _NpcUnit;
    public static NpcUnits NpcUnits => _NpcUnit;

    private new void Awake()
    {
        base.Awake();
        
        // 初始化GameFacade
        _playerUnit = (PlayerUnits) gameObject.AddComponent<PlayerUnits>();
        _MonsterUnit = (MonsterUnits) gameObject.AddComponent<MonsterUnits>();
        _NpcUnit = (NpcUnits) gameObject.AddComponent<NpcUnits>();
    }

    // 从对象池获取Unit
    // 没创建时从Resources加载
    public GameObject TryGet(PoolType type,string prefabName,string name = null)
    {
        return Pool.Instance.TryGet(type,prefabName,name);
    }

    // 将Unit放回对象池
    public void Push(PoolType type,string name, GameObject item)
    {
        Pool.Instance.Push(type,name, item);
    }
}