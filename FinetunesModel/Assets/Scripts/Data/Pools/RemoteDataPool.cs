using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LitJson;
using LocalData;
using System;

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

    public CompanyDataList companys;

    public override void Init()
    {

    }

    public override void Save()
    {

    }

    /// <summary>
    /// ���ع�˾����
    /// </summary>
    public void LoadCompanyData(Action onComplete)
    {
        PanelManager.Instance.ShowLoading();
        LocalUrlData data = LocalUrlDataPool.Instance.GetLocalUrlDataById(1);
        MyNet.instance.AddNode(data, (result) => {
            OnLoadCompanyDataSuccess(result);
            PanelManager.Instance.HideLoading();
            onComplete();
        });
    }

    public void OnLoadCompanyDataSuccess(SuccessResult result)
    {
        companys = MyConvert.ToText<CompanyDataList>(result);
        LocalDataPool.Instance.ToCompanyEntryData(companys);
    }
}
