using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LitJson;

public class RemoteUrlDataPool : DataPoolBase
{
    private static RemoteUrlDataPool instance;

    // �����캯����Ϊ˽�У���������Ͳ������ⲿ��ʵ����
    private RemoteUrlDataPool()
    {
        Init();
    }

    public static RemoteUrlDataPool Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new RemoteUrlDataPool();
            }
            return instance;
        }
    }

    public override void Init()
    {

    }

    public override void Save()
    {

    }
}
