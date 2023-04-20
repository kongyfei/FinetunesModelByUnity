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

    // 将构造函数设为私有，这样该类就不能在外部被实例化
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
    /// 加载公司数据
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
        LogExtension.LogFail($"不存在{name}公司");
        return null;
    }

    /// <summary>
    /// 加载公司Url信息
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
    /// 加载模型数据
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
    /// 加载URL数据
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
            LogExtension.LogFail($"已存在{urlData.url}数据");
        }
    }

    /// <summary>
    /// 获取该公司的微调事件列表
    /// </summary>
    /// <param name="companyName">公司名</param>
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
                    LogExtension.LogFail($"{company}不存在{id}的url数据");
                    return null;
                }
            }
            LogExtension.LogFail($"{company}不存在{function}的url数据");
            return null;
        }
        return null;
    }
}

public enum UrlType
{
    SHOW_FINETUNE_EVENTS,
}
