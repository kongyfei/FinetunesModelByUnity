using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// ���ݹ�����
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
/// �������
/// </summary>
public enum DataCategory
{
    LocalData, //��������
    LocalUrlData,  //���ص��������罻��������
    RemoteData,  //Զ������
    RemoteUrlData,  //Զ���������罻��������
}
