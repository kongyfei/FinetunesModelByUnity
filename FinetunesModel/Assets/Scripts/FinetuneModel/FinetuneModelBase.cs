using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.Networking;

/// <summary>
/// ΢��ģ�͵Ļ���
/// </summary>
public abstract class FinetuneModelBase : MonoBehaviour
{
    protected IEnumerator UploadFileBase(string url, string apiKey, string purpose, string filePath)
    {
        WWWForm fileds = new WWWForm();
        fileds.AddField("purpose", purpose);
        fileds.AddField("file", filePath);

        // ���� UnityWebRequest �����������󷽷�������ͷ
        UnityWebRequest request = UnityWebRequest.Post(url, fileds);
        request.SetRequestHeader("Authorization", $"Bearer {apiKey}");

        //�ϴ��ļ�����
        byte[] fileData = File.ReadAllBytes(filePath);
        request.uploadHandler = new UploadHandlerRaw(fileData);

        // �������󲢵ȴ���Ӧ
        yield return request.SendWebRequest();

        // ������Ӧ
        if (request.result != UnityWebRequest.Result.Success)
        {
            Debug.LogError($"Upload failed: {request.error}");
        }
        else
        {
            Debug.Log("Upload successful!");
        }
    }

    public abstract void UploadFile(string filePath);
    public abstract void ConvertFile(string filePath);

}