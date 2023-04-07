using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Reflection;
using System;

/// <summary>
/// ���ݹ�����
/// </summary>
public class DataManager : MonoSingleton<DataManager>
{ 
    private Dictionary<DataCategory, DataPoolBase> pools;

    private void Start()
    {
        pools = new Dictionary<DataCategory, DataPoolBase>();

        //pools.Add(DataCategory.LocalData, LocalDataPool.Instance);
        pools.Add(DataCategory.LocalUrlData, LocalUrlDataPool.Instance);
        //pools.Add(DataCategory.LocalData, LocalDataPool.Instance);
        //pools.Add(DataCategory.LocalData, LocalDataPool.Instance);
    }

    private void OnDestroy()
    {
        Save();
    }

    private void Save()
    {
        foreach (var item in pools.Keys)
        {
            pools[item].Save();
        }
    }
}

/// <summary>
/// �������
/// </summary>
public enum DataCategory
{
    LocalData, //��������
    LocalUrlData,  //���ص��������罻��������
    RemoteData,  //Զ������
    RemoteUrlData,  //Զ���������罻��������
}
