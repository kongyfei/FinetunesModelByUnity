using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Reflection;
using System;

/// <summary>
/// 数据管理器
/// </summary>
public class DataManager : MonoSingleton<DataManager>
{ 
    public Dictionary<DataCategory, DataPoolBase> pools;

    private void Start()
    {
        pools.Add(DataCategory.LocalData, LocalDataPool.Instance);
        pools.Add(DataCategory.LocalUrlData, LocalUrlDataPool.Instance);
        //pools.Add(DataCategory.LocalData, LocalDataPool.Instance);
        //pools.Add(DataCategory.LocalData, LocalDataPool.Instance);
    }
}

/// <summary>
/// 数据类别
/// </summary>
public enum DataCategory
{
    LocalData, //本地数据
    LocalUrlData,  //本地的用于网络交互的数据
    RemoteData,  //远端数据
    RemoteUrlData,  //远端用于网络交互的数据
}
