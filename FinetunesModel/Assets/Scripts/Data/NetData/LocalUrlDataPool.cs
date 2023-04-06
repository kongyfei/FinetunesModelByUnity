using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LocalData;

/// <summary>
/// 本地数据池
/// </summary>
public class LocalUrlDataPool : DataPoolBase
{
    private static LocalUrlDataPool instance;

    // 将构造函数设为私有，这样该类就不能在外部被实例化
    private LocalUrlDataPool()
    {
        Init();
    }

    public static LocalUrlDataPool Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new LocalUrlDataPool();
            }
            return instance;
        }
    }

    public override void Init()
    {
        // 获取 MyUrlDataSet 对象的实例
        MyUrlDataSet myUrlDataSet = Resources.Load<MyUrlDataSet>("MyUrlDataSet");

        // 获取 MyUrlDataSet 对象中的 myUrlDatas 列表
        List<LocalUrlData> urlDatas = myUrlDataSet.myUrlDatas;
    }
}
