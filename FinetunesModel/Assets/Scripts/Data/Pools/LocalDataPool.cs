using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LocalData;
using LitJson;

/// <summary>
/// �������ݳ�
/// </summary>
public class LocalDataPool : DataPoolBase
{
    private static LocalDataPool instance;

    // �����캯����Ϊ˽�У���������Ͳ������ⲿ��ʵ����
    private LocalDataPool() 
    {
        Init();
    }

    public static LocalDataPool Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new LocalDataPool();
            }
            return instance;
        }
    }

    public Dictionary<string, string> token;

    public override void Init()
    {

    }

    public override void Save()
    {

    }
}
