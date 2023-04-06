using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LocalData;

/// <summary>
/// �������ݳ�
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
        // ��ȡ MyUrlDataSet �����ʵ��
        MyUrlDataSet myUrlDataSet = Resources.Load<MyUrlDataSet>("MyUrlDataSet");

        // ��ȡ MyUrlDataSet �����е� myUrlDatas �б�
        localUrlDatas = myUrlDataSet.myUrlDatas;
    }
}
