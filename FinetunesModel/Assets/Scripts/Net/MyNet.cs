using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using System;
using LocalData;

/// <summary>
/// ����ͨ��ģ��
/// </summary>
public class MyNet : MonoBehaviour
{
    public static MyNet instance;

    [Header("�ش�����")]
    public int RETRY;
    [Header("��ʱʱ��")]
    public int TIMEOUT;
    [Header("�ض������")]
    public int RL;

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

    public void AddNode(LocalUrlData urlData, Action<SuccessResult> successHandle = null, Action<FailResult> failHandle = null)
    {
        NetNode node = new NetNode(urlData, successHandle, failHandle);
        StartAsycnNet(node);
    }

    /// <summary>
    /// �����첽ͨ�Žӿ�
    /// </summary>
    /// <param name="url">��ַ</param>
    /// <param name="method">����</param>
    /// <param name="paras">����ͷ</param>
    /// <param name="datas">��������</param>
    /// <param name="successHandle">���ʳɹ�����</param>
    /// <param name="failHandle">����ʧ�ܴ���</param>
    private void StartAsycnNet(NetNode node)
    {
        LocalUrlData urlData = node.data;
        StartCoroutine(AsycnNet(urlData.url, urlData.method, urlData.GetHeads(), urlData.GetFields(), urlData.GetDatas(), node.successHandle, node.failHandle));
    }

    private void Setting(UnityWebRequest request)
    {
        request.timeout = TIMEOUT;
    }

    /// <summary>
    /// �����첽ͨ��
    /// </summary>
    /// <param name="url">��ַ</param>
    /// <param name="method">����</param>
    /// <param name="paras">����ͷ</param>
    /// <param name="datas">��������</param>
    /// <param name="successHandle">���ʳɹ�����</param>
    /// <param name="failHandle">����ʧ�ܴ���</param>
    private IEnumerator AsycnNet(string url, string method, Dictionary<string, string> heads, WWWForm form, byte[] datas, Action<SuccessResult> successHandle, Action<FailResult> failHandle)
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

                LogExtension.LogSuccess("��ʼ����");

                yield return request.SendWebRequest();
                if (request.result != UnityWebRequest.Result.Success)
                {
                    LogExtension.LogFail("������룺" + request.responseCode);
                    LogExtension.LogFail("������Ϣ��" + request.error);
                    if (failHandle != null)
                    {
                        FailResult result = new FailResult(request.responseCode, request.error);
                        failHandle(result);
                    }
                }
                else
                {
                    string responseJson = request.downloadHandler.text;
                    LogExtension.LogSuccess(responseJson);
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
            LogExtension.LogFail("δ��ȷ��������");
        }
    }
}

/// <summary>
/// ͨ������ڵ�
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
/// �ڵ�״̬
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
/// �ڵ����ȼ�
/// </summary>
public enum NetNodePriority
{
    LineUp,
    RightNow,
}

/// <summary>
/// �Զ��巵�ؽ��
/// </summary>
public class CustomResult
{
    public long responseCode;
}

/// <summary>
/// ʧ�ܽ��
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
/// �ɹ����
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
/// �ɹ�����������
/// </summary>
public enum SuccessResultType
{
    Text,
    Sprite,
}
