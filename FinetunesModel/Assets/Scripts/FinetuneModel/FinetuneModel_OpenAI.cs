using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinetuneModel_OpenAI : FinetuneModelBase
{
    private const string UPLOADFILE_URL = "https://api.openai.com/v1/files";
    private const string APL_KEY = "";

    public override void UploadFile(string filePath)
    {
        StartCoroutine(UploadFileBase(UPLOADFILE_URL, APL_KEY, "fine-tune", filePath));
    }

    public override void ConvertFile(string filePath)
    {

    }
}
