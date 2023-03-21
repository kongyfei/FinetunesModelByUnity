using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 公司信息
/// </summary>
public class NetCompanyData : NetDataBase
{
    private string _id;
    private string _name;

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
}
