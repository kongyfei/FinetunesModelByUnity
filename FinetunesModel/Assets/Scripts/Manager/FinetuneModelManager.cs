using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading;

public class FinetuneModelManager : MonoBehaviour
{
    FinetuneModelBase finetuneModel;
    public string filePath;
    public string filePath1;
    public string fileId;
    void Start()
    {
        finetuneModel = gameObject.AddComponent<FinetuneModel_OpenAI>();
    }

    public void UploadFile()
    {
        finetuneModel.UploadFile(filePath, Fail, Success);
    }

    public void ConvertFileAsync()
    {
        // ����һ���µ��̣߳�����������Ϊִ��һ������
        Thread thread = new Thread(ConvertFile);
        // ��ʼ�߳�
        thread.Start();
    }

    private void ConvertFile()
    {
        filePath = finetuneModel.ConvertFile(filePath1);
    }

    public void GetFileList()
    {
        finetuneModel.GetFileList(Fail);
    }

    public void FinetuneModel()
    {
        finetuneModel.FinetuneModel(fileId, Fail, Success);
    }

    public void GetFinetuneModelList()
    {
        finetuneModel.GetFinetuneModelList(Fail);
    }

    public void Fail(string content)
    {
        Tool.DebugExtension.LogFail(content);
    }

    public void Success(string content)
    {
        Tool.DebugExtension.LogSuccess("�ɹ�");
        Debug.Log(content);
    }
}
