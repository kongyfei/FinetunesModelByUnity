using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using System;

/// <summary>
/// 网络通信模块
/// </summary>
public class MyNet : MonoBehaviour
{
    public static MyNet instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
        DontDestroyOnLoad(this.gameObject);
    }

    /// <summary>
    /// 网络异步通信接口
    /// </summary>
    /// <param name="url">地址</param>
    /// <param name="method">方法</param>
    /// <param name="paras">参数头</param>
    /// <param name="datas">传递数据</param>
    /// <param name="successHandle">访问成功处理</param>
    /// <param name="failHandle">访问失败处理</param>
    public void StartAsycnNet(string url, string method, Dictionary<string, string> paras, byte[] datas, Action<object> successHandle, Action<string> failHandle)
    {
        StartCoroutine(AsycnNet(url, method, paras, datas, successHandle, failHandle));
    }

    //public void StartAsycnNet(MyUrlData data, Action<object> successHandle, Action<string> failHandle)
    //{
    //    StartCoroutine(data.url, data.method, data.)
    //}

    /// <summary>
    /// 网络异步通信
    /// </summary>
    /// <param name="url">地址</param>
    /// <param name="method">方法</param>
    /// <param name="paras">参数头</param>
    /// <param name="datas">传递数据</param>
    /// <param name="successHandle">访问成功处理</param>
    /// <param name="failHandle">访问失败处理</param>
    private IEnumerator AsycnNet(string url, string method, Dictionary<string, string> heads, byte[] datas, Action<object> successHandle, Action<string> failHandle)
    {
        using (var request = new UnityWebRequest(url, method))
        {
            if (datas != null)
            {
                request.uploadHandler = new UploadHandlerRaw(datas);
            }
            request.downloadHandler = new DownloadHandlerBuffer();

            if (heads != null && heads.Count > 0)
            {
                foreach (var item in heads.Keys)
                {
                    request.SetRequestHeader(item, heads[item]);
                }
            }

            yield return request.SendWebRequest();
            if (request.result != UnityWebRequest.Result.Success)
            {
                //Tool.DebugExtension.LogFail("访问失败:");
                Debug.Log(request.error);
                if (failHandle != null)
                {
                    failHandle(request.error);
                }
            }
            else
            {
                //Tool.DebugExtension.LogSuccess("访问成功:");
                string responseJson = request.downloadHandler.text;
                Debug.Log(responseJson);
                if (successHandle != null)
                {
                    successHandle(responseJson);
                }
            }
        }
    }
}
