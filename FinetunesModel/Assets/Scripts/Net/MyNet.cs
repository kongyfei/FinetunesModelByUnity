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
    public void StartAsycnNet(string url, string method, Dictionary<string, string> paras, byte[] datas, Action<object> successHandle, Action<string> failHandle)
    {
        StartCoroutine(AsycnNet(url, method, paras, datas, successHandle, failHandle));
    }

    //public void StartAsycnNet(MyUrlData data, Action<object> successHandle, Action<string> failHandle)
    //{
    //    StartCoroutine(data.url, data.method, data.)
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
}
