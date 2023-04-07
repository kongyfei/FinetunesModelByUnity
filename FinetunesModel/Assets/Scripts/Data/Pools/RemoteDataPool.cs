using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LitJson;

public class RemoteDataPool : DataPoolBase
{
    private static RemoteDataPool instance;

    // 将构造函数设为私有，这样该类就不能在外部被实例化
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
