using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinetuneModelManager : MonoBehaviour
{
    FinetuneModelBase finetuneModel;
    public string filePath;

    void Start()
    {
        finetuneModel = gameObject.AddComponent<FinetuneModel_OpenAI>();
    }

    public void UploadFile()
    {
        finetuneModel.UploadFile(filePath);
    }
}
