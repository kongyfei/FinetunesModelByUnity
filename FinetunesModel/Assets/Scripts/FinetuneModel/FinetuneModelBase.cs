using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.Networking;

/// <summary>
/// 微调模型的基类
/// </summary>
public abstract class FinetuneModelBase : MonoBehaviour
{
    protected IEnumerator UploadFileBase(string url, string apiKey, string purpose, string filePath)
    {
        WWWForm fileds = new WWWForm();
        fileds.AddField("purpose", purpose);
        fileds.AddField("file", filePath);

        // 创建 UnityWebRequest 对象，设置请求方法和请求头
        UnityWebRequest request = UnityWebRequest.Post(url, fileds);
        request.SetRequestHeader("Authorization", $"Bearer {apiKey}");

        //上传文件内容
        byte[] fileData = File.ReadAllBytes(filePath);
        request.uploadHandler = new UploadHandlerRaw(fileData);

        // 发送请求并等待响应
        yield return request.SendWebRequest();

        // 处理响应
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