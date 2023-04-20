using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ModelEntry : LoopEntry
{
    public Text baseModelMessage;
    public Text usePrice;

    public Text name;
    public Text trainTime;
    public Text score;
    public Text trainPrice;
    public Text trainCost;
    public Text trainDataSet;

    public GameObject wait;
    public GameObject traing;
    public GameObject done;
    
    private void OnEnable()
    {
        
    }

    private void OnDisable()
    {
        
    }

    public void Refresh(ModelEntryData data)
    {
        wait.SetActive(false);
        traing.SetActive(false);
        done.SetActive(false);

        if (data.state == ModelTrainState.Wait)
        {
            wait.SetActive(true);
            baseModelMessage.text = data.baseModelMessage;
            trainPrice.text = data.trainPrice.ToString();
            usePrice.text = data.usePrice.ToString();
        }
        else if (data.state == ModelTrainState.Traing)
        {
            traing.SetActive(true);
        }
        else if (data.state == ModelTrainState.Done)
        {
            done.SetActive(true);
            name.text = data.name;
            trainTime.text = data.trainTime.ToString();
            score.text = data.score.ToString();
            trainPrice.text = data.trainPrice.ToString();
            trainCost.text = data.trainCost.ToString();
            trainDataSet.text = data.trainDataSet;
        }
    }
}

public class ModelEntryData
{
    //��ѵ����ʾ��Ϣ
    public string baseModelMessage;
    public float trainPrice;
    public float usePrice;

    //ѵ�������ʾ��Ϣ
    public string name;
    public int trainTime;//��λ��
    public float score;
    public float trainCost; //ѵ������
    public string trainDataSet;

    //ģ��״̬
    public ModelTrainState state;
}

public enum ModelTrainState
{
    Wait,
    Traing,
    Done,
}
