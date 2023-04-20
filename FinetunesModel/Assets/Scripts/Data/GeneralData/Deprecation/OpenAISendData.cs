using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenAISendData
{
    public List<OpenAISendData_CreateFinetuneModel> createFinetuneModel;
}

public class OpenAISendData_CreateFinetuneModel
{
    public string training_file;
    public string validation_file;
    public string model;
    public int n_epochs;
    public int batch_size;
    public float learning_rate_multiplier;
    public float prompt_loss_weight;
    public bool compute_classification_metrics;
    public int classification_n_classes;
    public string classification_positive_class;
    public float[] classification_betas;
    public string suffix;
}
