using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

/// <summary>
/// ����ͨ�Ų��Խű�
/// </summary>
public class MyNetTest : MonoBehaviour
{
    [Header("���԰�ť")]
    public Button testBtn;
    [Header("����")]
    public string url;
    [Header("ͨѶ����")]
    public string method;
    [Header("����ͷ��")]
    public List<string> paras_key;
    [Header("����ͷֵ")]
    public List<string> paras_value;
    private Dictionary<string, string> paras;
    [Header("������")]
    public string json;

    public void Test()
    {
        paras = new Dictionary<string, string>();
        for (int i = 0; i < paras.Count; i++)
        {
            paras.Add(paras_key[i], paras_value[i]);
        }

        //MyNet.instance.StartAsycnNet(url, method, paras, MyConvert.JsonToBytes(json), null, null);
    }
}
