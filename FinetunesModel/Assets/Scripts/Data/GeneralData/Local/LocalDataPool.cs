using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LocalData;

/// <summary>
/// 本地数据池
/// </summary>
public class LocalDataPool : DataPoolBase
{
    private static LocalDataPool instance;

    // 将构造函数设为私有，这样该类就不能在外部被实例化
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

    public override void Init()
    {
    }
}
