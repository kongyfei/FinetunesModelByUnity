using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

/// <summary>
/// 网络通信测试脚本
/// </summary>
public class MyNetTest : MonoBehaviour
{
    [Header("测试按钮")]
    public Button testBtn;
    [Header("链接")]
    public string url;
    [Header("通讯方法")]
    public string method;
    [Header("参数头键")]
    public List<string> paras_key;
    [Header("参数头值")]
    public List<string> paras_value;
    private Dictionary<string, string> paras;
    [Header("数据体")]
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
