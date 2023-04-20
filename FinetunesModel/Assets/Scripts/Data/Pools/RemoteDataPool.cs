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

    public List<CompanyData> companys;

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
        LocalUrlData data = LocalUrlDataPool.Instance.GetLocalUrlDataById(1);
        MyNet.instance.AddNode(data, (result) => {
            OnLoadCompanyDataSuccess(result);
            onComplete();
        });
    }

    public void OnLoadCompanyDataSuccess(SuccessResult result)
    {
        companys = MyConvert.ToText<List<CompanyData>>(result);
        LocalDataPool.Instance.ToCompanyEntryData(companys);
    }

    public CompanyData GetCompanyData(string name)
    {
        for (int i = 0; i < companys.Count; i++)
        {
            if (companys[i].name == name)
            {
                return companys[i];
            }
        }
        LogExtension.LogFail($"������{name}��˾");
        return null;
    }

    /// <summary>
    /// ���ع�˾Url��Ϣ
    /// </summary>
    public void LoadCompanyUrlMessage(string companyName, Action onComplete)
    {
        LocalUrlData data = LocalUrlDataPool.Instance.GetLocalUrlDataById(3);
        data.SetData("company_name", null, companyName);
        MyNet.instance.AddNode(data, (result) => {
            OnLoadCompanyUrlDataSuccess(companyName, result);
            onComplete();
        });
    }

    public void OnLoadCompanyUrlDataSuccess(string name, SuccessResult result)
    {
        CompanyData companyData = GetCompanyData(name);
        companyData.urlMessages = MyConvert.ToText<List<UrlMessage>>(result);
    }

    /// <summary>
    /// ����ģ������
    /// </summary>
    public void LoadModelData(string companyName,Action onComplete)
    {
        LocalUrlData data = LocalUrlDataPool.Instance.GetLocalUrlDataById(2);
        data.SetData("company_name", null, companyName);
        MyNet.instance.AddNode(data, (result) => {
            OnLoadModelDataSuccess(companyName, result);
            onComplete();
        });
    }

    public void OnLoadModelDataSuccess(string name, SuccessResult result)
    {
        CompanyData companyData = GetCompanyData(name);
        companyData.modelDatas = MyConvert.ToText<List<ModelData>>(result);
    }

    /// <summary>
    /// ����URL����
    /// </summary>
    public void LoadUrlData(string companyName, string urlId, Action onComplete)
    {
        LocalUrlData data = LocalUrlDataPool.Instance.GetLocalUrlDataById(4);
        data.SetData("company_name", null, companyName);
        data.SetData("url_id", null, urlId);
        MyNet.instance.AddNode(data, (result) => {
            OnLoadUrlDataSuccess(companyName, result);
            onComplete();
        });
    }

    public void OnLoadUrlDataSuccess(string companyName, SuccessResult result)
    {
        CompanyData companyData = GetCompanyData(companyName);
        UrlData urlData = MyConvert.ToText<UrlData>(result);
        if (!companyData.urlData.Contains(urlData))
        {
            companyData.urlData.Add(urlData);
        }
        else
        {
            LogExtension.LogFail($"�Ѵ���{urlData.url}����");
        }
    }

    /// <summary>
    /// ��ȡ�ù�˾��΢���¼��б�
    /// </summary>
    /// <param name="companyName">��˾��</param>
    public void GetFinetuneEventList(string companyName)
    {
        UrlData data = GetUrlData(companyName, UrlType.SHOW_FINETUNE_EVENTS.ToString());
    }

    public UrlData GetUrlData(string company, string function)
    {
        CompanyData companyData = GetCompanyData(company);
        if (companyData != null)
        {
            for (int i = 0; i < companyData.urlMessages.Count; i++)
            {
                if (function == companyData.urlMessages[i].funtion)
                {
                    int id = companyData.urlMessages[i].id;
                    for (int l = 0; l < companyData.urlData.Count; l++)
                    {
                        if (companyData.urlData[l].id == id)
                        {
                            return companyData.urlData[l];
                        }
                    }
                    LogExtension.LogFail($"{company}������{id}��url����");
                    return null;
                }
            }
            LogExtension.LogFail($"{company}������{function}��url����");
            return null;
        }
        return null;
    }
}

public enum UrlType
{
    SHOW_FINETUNE_EVENTS,
}
