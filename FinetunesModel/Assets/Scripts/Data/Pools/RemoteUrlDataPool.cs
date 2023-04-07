using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LitJson;

public class RemoteUrlDataPool : DataPoolBase
{
    private static RemoteUrlDataPool instance;

    // 将构造函数设为私有，这样该类就不能在外部被实例化
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
