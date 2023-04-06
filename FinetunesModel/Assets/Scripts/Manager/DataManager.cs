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
/// �������
/// </summary>
public enum DataCategory
{
    LocalData, //��������
    LocalUrlData,  //���ص��������罻��������
    RemoteData,  //Զ������
    RemoteUrlData,  //Զ���������罻��������
}
