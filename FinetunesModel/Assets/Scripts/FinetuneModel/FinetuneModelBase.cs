using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.Networking;
using FileData.OpenAI;
using System;

/// <summary>
/// 微调模型的基类
/// </summary>
public abstract class FinetuneModelBase : MonoBehaviour
{
    //转换后的训练数据的持久化存储路径
    protected string dataSetFilePath;

    private void Awake()
    {
        dataSetFilePath = Application.persistentDataPath + "/";
    }

    protected IEnumerator UploadFileBase(string url, string apiKey, string purpose, string filePath, Action<string> fail, Action<string> success)
    {
        WWWForm fileds = new WWWForm();
        fileds.AddField("purpose", purpose);
        //上传文件内容
        byte[] fileData = File.ReadAllBytes(filePath);
        fileds.AddBinaryData("file", fileData, Path.GetFileName(filePath), "application/jsonl");

        // 创建 UnityWebRequest 对象，设置请求方法和请求头
        UnityWebRequest request = UnityWebRequest.Post(url, fileds);
        request.SetRequestHeader("Authorization", $"Bearer {apiKey}");

        // 发送请求并等待响应
        yield return request.SendWebRequest();

        // 处理响应
        if (request.result != UnityWebRequest.Result.Success)
        {
            fail(request.error);
        }
        else
        {
            success(request.downloadHandler.text);
        }
    }

    protected IEnumerator GetFileListBase(string url, string apiKey, Action<string> fail, Action<string> success)
    {
        UnityWebRequest request = UnityWebRequest.Get(url);
        request.SetRequestHeader("Authorization", "Bearer " + apiKey);
        yield return request.SendWebRequest();

        if (request.result != UnityWebRequest.Result.Success)
        {
            fail(request.error);
        }
        else
        {
            success(request.downloadHandler.text);
        }
    }

    protected IEnumerator FinetuneBase(string url, string apiKey, string jsonData, Action<string> fail, Action<string> success)
    {
        // 创建 UnityWebRequest 对象，设置请求方法和请求头
        UnityWebRequest request = UnityWebRequest.Post(url, new WWWForm());
        request.SetRequestHeader("Authorization", $"Bearer {apiKey}");
        request.SetRequestHeader("Content-Type", "application/json");

        //需要手动转换，否则报错
        byte[] payloadBytes = System.Text.Encoding.UTF8.GetBytes(jsonData);
        request.uploadHandler = new UploadHandlerRaw(payloadBytes);

        // 发送请求并等待响应
        yield return request.SendWebRequest();

        // 处理响应
        if (request.result != UnityWebRequest.Result.Success)
        {
            fail(request.error);
        }
        else
        {
            success(request.downloadHandler.text);
        }
    }

    protected IEnumerator GetFinetuneModelListBase(string url, string apiKey, Action<string> fail, Action<string> success)
    {
        // 创建 UnityWebRequest 对象，设置请求方法和请求头
        UnityWebRequest request = UnityWebRequest.Get(url);
        request.SetRequestHeader("Authorization", $"Bearer {apiKey}");

        // 发送请求并等待响应
        yield return request.SendWebRequest();

        // 处理响应
        if (request.result != UnityWebRequest.Result.Success)
        {
            fail(request.error);
        }
        else
        {
            success(request.downloadHandler.text);
        }
    }

    public abstract void UploadFile(string filePath, Action<string> fail, Action<string> success);
    public abstract string ConvertFile(string filePath);
    public abstract void GetFileList(Action<string> fail);
    public abstract void FinetuneModel(string fileId, Action<string> fail, Action<string> success);
    public abstract void GetFinetuneModelList(Action<string> fail);
}