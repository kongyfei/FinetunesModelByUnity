using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 数据管理器
/// </summary>
public class DataManager : MonoBehaviour
{
    public DataManager instance;

    public Dictionary<DataCategory, DataPoolBase> pools;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
        DontDestroyOnLoad(this.gameObject);
    }

    private void Start()
    {
    }

    private void Init()
    {
        LocalDataPool localDataPool = new LocalDataPool();
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
