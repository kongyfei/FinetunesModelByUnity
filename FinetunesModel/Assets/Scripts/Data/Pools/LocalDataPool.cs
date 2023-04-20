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

    //各ai平台访问token
    public Dictionary<string, string> token;




    //公司数据
    public List<CompanyEntryData> companyEntryDatas;
    public CompanyEntryData companyEntryData;
    //模型数据
    public List<ModelEntryData> modelEntryDatas;

    public override void Init()
    {
        companyEntryDatas = new List<CompanyEntryData>(10);
    }

    public override void Save()
    {

    }

    public void ToCompanyEntryData(List<CompanyData> list)
    {
        for (int i = 0; i < list.Count; i++)
        {
            companyEntryDatas.Add(new CompanyEntryData(list[i].name));
        }
    }

    public void SetCurCompanyEntryData(CompanyEntryData data)
    {
        companyEntryData = data;
    }
}
