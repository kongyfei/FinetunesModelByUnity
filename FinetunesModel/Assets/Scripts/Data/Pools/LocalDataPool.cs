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

    //��aiƽ̨����token
    public Dictionary<string, string> token;




    //��˾����
    public List<CompanyEntryData> companyEntryDatas;
    public CompanyEntryData companyEntryData;
    //ģ������
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
