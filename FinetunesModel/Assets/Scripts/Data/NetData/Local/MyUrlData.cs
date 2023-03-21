using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class MyUrlData
{
    [Header("ID")]
    [SerializeField]
    public int id;
    [Header("URL")]
    [SerializeField]
    public string url;
    [Header("����")]
    [SerializeField]
    public string method;
    [Header("��Ӧͷ")]
    [SerializeField]
    public List<string> heads; //ע����key��value����
    [Header("��Ӧ�ֶ�")]
    [SerializeField]
    public List<string> fields;
    [Header("���ݸ�ʽ")]
    [SerializeField]
    public string data;

    public Dictionary<string, string> GetHeads()
    {
        return ListToDic(fields);
    }

    public Dictionary<string, string> GetFileds()
    {
        return ListToDic(heads);
    }

    private Dictionary<string, string> ListToDic(List<string> list)
    {
        Dictionary<string, string> dictionary = new Dictionary<string, string>();
        for (int i = 0; i < list.Count; i++)
        {
            string[] keyValuePairs = list[i].Split(';');
            dictionary.Add(keyValuePairs[0], keyValuePairs[1]);
        }
        return dictionary;
    }


}
