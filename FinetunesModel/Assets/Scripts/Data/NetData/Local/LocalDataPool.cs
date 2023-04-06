using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LocalData;

/// <summary>
/// 本地数据池
/// </summary>
public class LocalDataPool : DataPoolBase
{
    public List<LocalUrlData> localUrlDatas;

    private LocalDataPool()
    {
        Init();
    }

    public override void Init()
    {
        // 获取 MyUrlDataSet 对象的实例
        MyUrlDataSet myUrlDataSet = Resources.Load<MyUrlDataSet>("MyUrlDataSet");

        // 获取 MyUrlDataSet 对象中的 myUrlDatas 列表
        localUrlDatas = myUrlDataSet.myUrlDatas;
    }
}
