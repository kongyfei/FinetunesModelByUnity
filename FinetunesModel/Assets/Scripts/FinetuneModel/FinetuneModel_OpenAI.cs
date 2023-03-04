using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinetuneModel_OpenAI : FinetuneModelBase
{
    private const string UPLOADFILE_URL = "https://api.openai.com/v1/files";
    private const string APL_KEY = "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";

    public override void UploadFile(string filePath)
    {
        StartCoroutine(UploadFileBase(UPLOADFILE_URL, APL_KEY, "fine-tune", filePath));
    }

    public override void ConvertFile(string filePath)
    {

    }
}
