using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LitJson;
using LocalData;
using System;
using System.Linq;

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

    public CompanyData GetCompanyData(string name)
    {
        for (int i = 0; i < companys.companys.Count; i++)
        {
            if (companys.companys[i].name == name)
            {
                return companys.companys[i];
            }
        }
        LogExtension.LogFail($"������{name}��˾");
        return null;
    }

    /// <summary>
    /// ���ع�˾Url��Ϣ
    /// </summary>
    public void LoadCompanyUrlData(string companyName, Action onComplete)
    {
        PanelManager.Instance.ShowLoading();
        LocalUrlData data = LocalUrlDataPool.Instance.GetLocalUrlDataById(3);
        data.SetData("company_name", null, companyName);
        MyNet.instance.AddNode(data, (result) => {
            OnLoadCompanyUrlDataSuccess(companyName, result);
            PanelManager.Instance.HideLoading();
            onComplete();
        });
    }

    public void OnLoadCompanyUrlDataSuccess(string name, SuccessResult result)
    {
        CompanyData companyData = GetCompanyData(name);
        //companyData.modelDatas = MyConvert.ToText<>(result);
    }

    /// <summary>
    /// ����ģ������
    /// </summary>
    public void LoadModelData(string companyName,Action onComplete)
    {
        PanelManager.Instance.ShowLoading();
        LocalUrlData data = LocalUrlDataPool.Instance.GetLocalUrlDataById(2);
        data.SetData("company_name", null, companyName);
        MyNet.instance.AddNode(data, (result) => {
            OnLoadModelDataSuccess(companyName, result);
            PanelManager.Instance.HideLoading();
            onComplete();
        });
    }

    public void OnLoadModelDataSuccess(string name, SuccessResult result)
    {
        CompanyData companyData = GetCompanyData(name);
        companyData.modelDatas = MyConvert.ToText<List<ModelData>>(result);
    }
}
