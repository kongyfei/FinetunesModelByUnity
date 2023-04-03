using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using System;

/// <summary>
/// ����ͨ��ģ��
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
    /// �����첽ͨ�Žӿ�
    /// </summary>
    /// <param name="url">��ַ</param>
    /// <param name="method">����</param>
    /// <param name="paras">����ͷ</param>
    /// <param name="datas">��������</param>
    /// <param name="successHandle">���ʳɹ�����</param>
    /// <param name="failHandle">����ʧ�ܴ���</param>
    //public void StartAsycnNet(MyUrlData urlData, Action<object> successHandle, Action<string> failHandle)
    //{
    //    //StartCoroutine(AsycnNet());
    //}

    /// <summary>
    /// �����첽ͨ��
    /// </summary>
    /// <param name="url">��ַ</param>
    /// <param name="method">����</param>
    /// <param name="paras">����ͷ</param>
    /// <param name="datas">��������</param>
    /// <param name="successHandle">���ʳɹ�����</param>
    /// <param name="failHandle">����ʧ�ܴ���</param>
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

                LogExtension.LogSuccess("��ʼ����");

                yield return request.SendWebRequest();
                if (request.result != UnityWebRequest.Result.Success)
                {
                    //Tool.DebugExtension.LogFail("����ʧ��:");
                    Debug.Log(request.error);
                    if (failHandle != null)
                    {
                        failHandle(request.error);
                    }
                }
                else
                {
                    //Tool.DebugExtension.LogSuccess("���ʳɹ�:");
                    string responseJson = request.downloadHandler.text;
                    Debug.Log(responseJson);
                    if (successHandle != null)
                    {
                        successHandle(responseJson);
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
