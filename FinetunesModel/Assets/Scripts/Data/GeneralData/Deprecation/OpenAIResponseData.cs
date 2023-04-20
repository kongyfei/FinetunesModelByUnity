using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

/// <summary>
/// OpenAiÏìÓ¦Êý¾Ý
/// </summary>
[Serializable]
public class OpenAIResponseData
{
    public List<OpenAIResponseData_CreateFinetuneModel> createFinetuneModel;
}

[Serializable]
public class OpenAIResponseData_CreateFinetuneModel
{
    public string id;
    public string @object;
    public string model;
    public long created_at;
    public List<OpenAIResponseData_FineTuneEvent> events;
    public object fine_tuned_model;
    public OpenAIResponseData_Hyperparams hyperparams;
    public string organization_id;
    public List<object> result_files;
    public string status;
    public List<object> validation_files;
    public List<OpenAIResponseData_TrainingFile> training_files;
    public long updated_at;
}

[Serializable]
public class OpenAIResponseData_FineTuneEvent
{
    public string @object;
    public long created_at;
    public string level;
    public string message;
}

[Serializable]
public class OpenAIResponseData_Hyperparams
{
    public int batch_size;
    public float learning_rate_multiplier;
    public int n_epochs;
    public float prompt_loss_weight;
}

[Serializable]
public class OpenAIResponseData_TrainingFile
{
    public string id;
    public string @object;
    public int bytes;
    public long created_at;
    public string filename;
    public string purpose;
}
