using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using FileData.OpenAI;
using LitJson;
using UnityEngine.Networking;
using System;

public class FinetuneModel_OpenAI : FinetuneModelBase
{
    private const string FILE_URL = "https://api.openai.com/v1/files";
    private const string FINETUNE_URL = "https://api.openai.com/v1/fine-tunes";
    private const string APL_KEY = "";
    public TrainFileList trainFileList;
    public FinetuneModelList finetuneModelList;

    public override void UploadFile(string filePath, Action<string> fail, Action<string> success)
    {
        StartCoroutine(UploadFileBase(FILE_URL, APL_KEY, "fine-tune", filePath, fail, success));
    }

    public override string ConvertFile(string filePath)
    {
        if (!string.IsNullOrEmpty(filePath))
        {
            using (StreamReader reader = new StreamReader(filePath))
            {
                string writePath = Path.ChangeExtension(dataSetFilePath + Path.GetFileName(filePath), ".jsonl");
                using (StreamWriter writer = new StreamWriter(writePath, false))
                {
                    while (!reader.EndOfStream)
                    {
                        string line = reader.ReadLine();
                        string[] values = line.Split(',');
                        string prompt = values[0];
                        string completion = values[1];
                        string json = "{\"prompt\": \"" + prompt + "\", \"completion\": \"" + completion + "\"}";
                        writer.WriteLine(json);
                    }
                }
                return writePath;
            }
        }
        return null;
    }

    public override void GetFileList(Action<string> fail)
    {
        StartCoroutine(GetFileListBase(FILE_URL, APL_KEY, fail, GetFileListSuccess));
    }

    private void GetFileListSuccess(string content)
    {
        trainFileList = JsonMapper.ToObject<TrainFileList>(content);
        Tool.DebugExtension.LogSuccess("成功获取文件列表:\n");
        Debug.Log(trainFileList.ToString());
    }

    public override void FinetuneModel(string fileId, Action<string> fail, Action<string> success)
    {
        string jsonData = "{\"training_file\":\"" + fileId + "\"}";
        StartCoroutine(FinetuneBase(FINETUNE_URL, APL_KEY, jsonData, fail, success));
    }

    private void GetFinetuneModelListSuccess(string content)
    {
        finetuneModelList = JsonMapper.ToObject<FinetuneModelList>(content);
        Tool.DebugExtension.LogSuccess("成功获取微调模型列表:\n");
        Debug.Log(finetuneModelList.ToString());
    }

    public override void GetFinetuneModelList(Action<string> fail)
    {
        StartCoroutine(GetFinetuneModelListBase(FINETUNE_URL, APL_KEY, fail, GetFinetuneModelListSuccess));
    }
}
