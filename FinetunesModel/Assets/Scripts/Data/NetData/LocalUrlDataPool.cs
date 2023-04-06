using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LocalData;

/// <summary>
/// �������ݳ�
/// </summary>
public class LocalUrlDataPool : DataPoolBase
{
    private static LocalUrlDataPool instance;

    // �����캯����Ϊ˽�У���������Ͳ������ⲿ��ʵ����
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
        // ��ȡ MyUrlDataSet �����ʵ��
        MyUrlDataSet myUrlDataSet = Resources.Load<MyUrlDataSet>("MyUrlDataSet");

        // ��ȡ MyUrlDataSet �����е� myUrlDatas �б�
        List<LocalUrlData> urlDatas = myUrlDataSet.myUrlDatas;
    }
}
