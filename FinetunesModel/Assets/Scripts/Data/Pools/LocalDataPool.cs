using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LocalData;
using LitJson;

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

    public Dictionary<string, string> token;
    public List<CompanyEntryData> companyEntryDatas;

    public override void Init()
    {
        companyEntryDatas = new List<CompanyEntryData>(10);
    }

    public override void Save()
    {

    }

    public void ToCompanyEntryData(CompanyDataList list)
    {
        List<CompanyData> companyDatas = list.companys;
        for (int i = 0; i < companyDatas.Count; i++)
        {
            companyEntryDatas.Add(new CompanyEntryData(companyDatas[i].name));
        }
    }
}
