using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LocalData;
using LitJson;

/// <summary>
/// �������ݳ�
/// </summary>
public class LocalDataPool : DataPoolBase
{
    private static LocalDataPool instance;

    // �����캯����Ϊ˽�У���������Ͳ������ⲿ��ʵ����
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
