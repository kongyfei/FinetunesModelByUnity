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

    public List<LocalUrlData> urlDatas;

    public override void Init()
    {
        // ��ȡ MyUrlDataSet �����ʵ��
        MyUrlDataSet myUrlDataSet = Resources.Load<MyUrlDataSet>("Data/MyUrlDataSet");

        // ��ȡ MyUrlDataSet �����е� myUrlDatas �б�
        urlDatas = myUrlDataSet.myUrlDatas;

        string url = myUrlDataSet.url;

        for (int i = 0; i < urlDatas.Count; i++)
        {
            urlDatas[i].url = url + urlDatas[i].url;
        }
    }

    public override void Save()
    {

    }

    public LocalUrlData GetLocalUrlDataById(int id)
    {
        for (int i = 0; i < urlDatas.Count; i++)
        {
            if (urlDatas[i].id == id)
            {
                return urlDatas[i];
            }
        }

        LogExtension.LogFail($"û�д�{id}��localurldata");
        return null;
    }
}
