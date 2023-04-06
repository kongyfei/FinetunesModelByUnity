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

    [Header("重传次数")]
    public int RETRY;
    [Header("超时时间")]
    public int TIMEOUT;
    [Header("重定向次数")]
    public int RL;

    private int retryNode;

    private List<NetNode> netNodes;
    private List<NetNode> retryNetNodes;

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

        netNodes = new List<NetNode>();
        retryNetNodes = new List<NetNode>();
    }

    private void Update()
    {
        if (netNodes.Count > 0)
        {
            for (int i = 0; i < netNodes.Count; i++)
            {
                if (netNodes[i].state == NetNodeState.Retry)
                {
                    RetryNode(netNodes[i]);
                }
            }
        }
    }

    public void AddNode(LocalUrlData urlData, Action<SuccessResult> successHandle = null, Action<FailResult> failHandle = null)
    {
        NetNode node = new NetNode(urlData, successHandle, failHandle);
        netNodes.Add(node);
        StartCoroutine(AsycnNet(node));
    }

    private void RetryNode(NetNode node)
    {
        StartCoroutine(AsycnNet(node));
    }

    private void Setting(UnityWebRequest request)
    {
        if (TIMEOUT != 0)
            request.timeout = TIMEOUT;
        if (RL != 0)
            request.redirectLimit = RL;
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
    private IEnumerator AsycnNet(NetNode node)
    {
        LocalUrlData data = node.data;
        string url = data.url;
        string method = data.method;
        Dictionary<string, string> heads = data.GetHeads();
        WWWForm form = data.GetFields();
        byte[] datas = data.GetDatas();
        Action<SuccessResult> successHandle = node.successHandle;
        Action<FailResult> failHandle = node.failHandle;

        node.state = NetNodeState.Start;

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

        Setting(request);

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

                node.state = NetNodeState.Doing;

                yield return request.SendWebRequest();
                if (request.result != UnityWebRequest.Result.Success)
                {   
                    LogExtension.LogFail("错误代码：" + request.responseCode);
                    LogExtension.LogFail("错误信息：" + request.error);

                    if (node.retry >= RETRY)
                    {
                        node.state = NetNodeState.Fail;
                        if (netNodes.Contains(node))
                        {
                            netNodes.Remove(node);
                        }

                        if (failHandle != null)
                        {
                            FailResult result = new FailResult(request.responseCode, request.error);
                            failHandle(result);
                        }
                    }
                    else
                    {
                        node.retry++;
                        node.state = NetNodeState.Retry;
                    }
                }
                else
                {
                    string responseJson = request.downloadHandler.text;
                    LogExtension.LogSuccess(responseJson);

                    node.state = NetNodeState.Success;
                    if (netNodes.Contains(node))
                    {
                        netNodes.Remove(node);
                    }

                    if (successHandle != null)
                    {
                        SuccessResult result = new SuccessResult(request.responseCode, SuccessResultType.Text, responseJson);
                        successHandle(result);
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
    public Action<SuccessResult> successHandle;
    public Action<FailResult> failHandle;

    public NetNode(LocalUrlData data, Action<SuccessResult> successHandle = null, Action<FailResult> failHandle = null)
    {
        retry = 0;
        this.data = data;
        this.successHandle = successHandle;
        this.failHandle = failHandle;
    }
}

/// <summary>
/// 节点状态
/// </summary>
public enum NetNodeState
{
    None,
    Waiting, //等待
    Start, //开始
    Doing, //进行中
    Success, //成功
    Fail, //失败
    Retry, //重试
}

/// <summary>
/// 节点优先级
/// </summary>
public enum NetNodePriority
{
    LineUp,
    RightNow,
}

/// <summary>
/// 自定义返回结果
/// </summary>
public class CustomResult
{
    public long responseCode;
}

/// <summary>
/// 失败结果
/// </summary>
public class FailResult : CustomResult
{
    public string error;

    public FailResult(long responseCode, string error)
    {
        base.responseCode = responseCode;
        this.error = error;
    }
}

/// <summary>
/// 成功结果
/// </summary>
public class SuccessResult : CustomResult
{
    public SuccessResultType type;
    public object result;

    public SuccessResult(long responseCode, SuccessResultType type, object result)
    {
        base.responseCode = responseCode;
        this.type = type;
        this.result = result;
    }
}

/// <summary>
/// 成功请求结果类型
/// </summary>
public enum SuccessResultType
{
    Text,
    Sprite,
}
