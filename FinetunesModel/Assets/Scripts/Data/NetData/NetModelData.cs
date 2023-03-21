using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 公司支持的微调模型信息
/// </summary>
public class NetModelData : NetDataBase
{
    private string _id;
    private string _name;
    private string _description;
    private string _function;
    private double _trainPrice;
    private double _usePrice;

    public string Id
    {
        get { return _id; }
        set { _id = value; }
    }

    public string Name
    {
        get { return _name; }
        set { _name = value; }
    }

    public string Description
    {
        get { return _description; }
        set { _description = value; }
    }

    public string Function
    {
        get { return _function; }
        set { _function = value; }
    }

    public double TrainPrice
    {
        get { return _trainPrice; }
        set { _trainPrice = value; }
    }

    public double UsePrice
    {
        get { return _usePrice; }
        set { _usePrice = value; }
    }
}
