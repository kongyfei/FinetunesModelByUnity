using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LitJson;

public class RemoteDataPool : DataPoolBase
{
    private static RemoteDataPool instance;

    // �����캯����Ϊ˽�У���������Ͳ������ⲿ��ʵ����
    private RemoteDataPool()
    {
        Init();
    }

    public static RemoteDataPool Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new RemoteDataPool();
            }
            return instance;
        }
    }

    public List<CompanyData> companyDatas;

    public override void Init()
    {

    }

    public override void Save()
    {

    }
}
