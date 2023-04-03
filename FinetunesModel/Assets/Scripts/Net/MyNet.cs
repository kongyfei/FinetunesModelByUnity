using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using System;
using LocalData;

/// <summary>
/// 网络通信模块
/// </summary>
public class MyNet : MonoBehaviour
{
    public static MyNet instance;

    public int retry;
    public float timeout;
    public int redirectLimit;

    private Queue<NetNode> netNodes;

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

        netNodes = new Queue<NetNode>();
    }

    private void Update()
    {

    }

    public void AddNode(LocalUrlData urlData, NetNodePriority priority = NetNodePriority.LineUp)
    {
        NetNode node = new NetNode(urlData, priority);
        switch (node.priority)
        {
            case NetNodePriority.LineUp:
                netNodes.Enqueue(node);
                break;
            //case NetNodePriority.RightNow:

        }
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
    public void StartAsycnNet(LocalUrlData urlData, Action<object> successHandle, Action<string> failHandle)
    {
        StartCoroutine(AsycnNet(urlData.url, urlData.method, urlData.GetHeads(), urlData.GetFields(), urlData.GetDatas(), successHandle, failHandle));
    }

    /// <summary>
    /// 网络异步通信
    /// </summary>
    /// <param name="url">地址</param>
    /// <param name="method">方法</param>
    /// <param name="paras">参数头</param>
    /// <param name="datas">传递数据</param>
    /// <param name="successHandle">访问成功处理</param>
    /// <param name="failHandle">访问失败处理</param>
    private IEnumerator AsycnNet(string url, string method, Dictionary<string, string> heads, WWWForm form, byte[] datas, Action<object> successHandle, Action<string> failHandle)
    {
        UnityWebRequest request;
        switch (method)
        {
            case "GET":
                request = new UnityWebRequest(url, method);
                break;
            case "POST":
                if (form != null)
                {
                    request = UnityWebRequest.Post(url, form);
                }
                else
                {
                    request = new UnityWebRequest(url, method);
                }
                break;
            default:
                request = null;
                break;
        }
        if (request != null)
        {
            using (request)
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

                LogExtension.LogSuccess("开始传输");

                yield return request.SendWebRequest();
                if (request.result != UnityWebRequest.Result.Success)
                {
                    LogExtension.LogFail(request.error);
                    if (failHandle != null)
                    {
                        failHandle(request.error);
                    }
                }
                else
                {
                    string responseJson = request.downloadHandler.text;
                    LogExtension.LogSuccess(responseJson);
                    if (successHandle != null)
                    {
                        successHandle(responseJson);
                    }
                }
            }
        }
        else
        {
            LogExtension.LogFail("未正确设置网络");
        }
    }
}

/// <summary>
/// 通信网络节点
/// </summary>
public class NetNode
{
    public int retry;
    public LocalUrlData data;
    public NetNodeState state = NetNodeState.None;
    public NetNodePriority priority = NetNodePriority.LineUp;

    public NetNode(LocalUrlData data, NetNodePriority priority)
    {
        retry = 0;
        this.data = data;
        this.priority = priority;
    }
}

/// <summary>
/// 节点状态
/// </summary>
public enum NetNodeState
{
    None,
    Waiting,
    Loading,
    Retry,
    Success,
    Fail,
}

/// <summary>
/// 节点优先级
/// </summary>
public enum NetNodePriority
{
    LineUp,
    RightNow,
}
