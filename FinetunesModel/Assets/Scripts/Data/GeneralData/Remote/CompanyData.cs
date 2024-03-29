using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using LocalData;

/// <summary>
/// 公司信息数据
/// </summary>
[Serializable]
public class CompanyData
{
    public int id;
    public string name;

    public CompanyMessageData companyMessageData;

    public List<ModelData> modelDatas;
    public List<UrlMessage> urlMessages;
    public List<UrlData> urlData;

    public override string ToString()
    {
        string result = "ID: " + id + "\n";
        result += "Name: " + name + "\n";
        result += "Company Message Data:\n";
        if (companyMessageData != null)
        {
            result += companyMessageData.ToString() + "\n";
        }
        result += "Model Data:\n";
        if (modelDatas != null)
        {
            foreach (ModelData modelData in modelDatas)
            {
                result += modelData.ToString() + "\n";
            }
        }
        result += "URL Messages:\n";
        if (urlMessages != null)
        {
            foreach (var url in urlMessages)
            {
                result += url.ToString();
            }
        }
        result += "URL Data:\n";
        if (urlData != null)
        {
            result += urlData.ToString() + "\n";
        }
        return result;
    }

}

/// <summary>
/// 公司其他信息数据
/// </summary>
[Serializable]
public class CompanyMessageData
{
    
}

/// <summary>
/// 模型数据
/// </summary>
[Serializable]
public class ModelData
{
    public int id;
    public string name;
    public string des;
    public string function;
    public string train_price;
    public string use_price;

    public override string ToString()
    {
        string result = "ID: " + id + "\n";
        result += "Name: " + name + "\n";
        result += "Description: " + des + "\n";
        result += "Function: " + function + "\n";
        result += "Training Price: " + train_price + "\n";
        result += "Usage Price: " + use_price + "\n";
        return result;
    }
}

/// <summary>
/// Url信息
/// </summary>
public class UrlMessage
{
    public int id;
    public string funtion;
    public string url;

    public override string ToString()
    {
        return $"Id: {id}, Function: {funtion}, URL: {url}";
    }
}

/// <summary>
/// url数据
/// </summary>
[Serializable]
public class UrlData
{
    public int id;
    public string url;
    public string method;
    public bool format;
    public List<UrlReqPara> req;
    public List<UrlResPara> res;

    public override string ToString()
    {
        string result = "ID: " + id + "\n";
        result += "URL: " + url + "\n";
        result += "Method: " + method + "\n";
        result += "Format: " + format.ToString() + "\n";
        result += "Request Parameters:\n";
        if (req != null)
        {
            foreach (UrlReqPara reqPara in req)
            {
                result += reqPara.ToString() + "\n";
            }
        }
        result += "Response Parameters:\n";
        if (res != null)
        {
            foreach (UrlResPara resPara in res)
            {
                result += resPara.ToString() + "\n";
            }
        }
        return result;
    }

    public override bool Equals(object obj)
    {
        if (obj == null || GetType() != obj.GetType())
        {
            return false;
        }

        UrlData other = (UrlData)obj;

        if (id != other.id)
        {
            return false;
        }

        return true;
    }

    /// <summary>
    /// 转换成LocalUrlData
    /// </summary>
    /// <returns>LocalUrlData</returns>
    //public LocalUrlData ToLocalUrlData(params object[] paras)
    //{
    //    LocalUrlData localUrlData = new LocalUrlData();
    //    localUrlData.SetUrl(paras);
    //    localUrlData.method = method;
    //}
}

/// <summary>
/// url请求参数
/// </summary>
[Serializable]
public class UrlReqPara
{
    public string para_name;
    public string para_type;
    public string para_details;
    public DataType range_list;
    public bool is_must;
    public string defaultValue;

    public override string ToString()
    {
        string result = "Parameter Name: " + para_name + "\n";
        result += "Parameter Type: " + para_type + "\n";
        result += "Parameter Details: " + para_details + "\n";
        if (range_list != null)
        {
            result += "Range List: " + range_list.ToString() + "\n";
        }
        result += "Is Required: " + (is_must ? "Yes" : "No") + "\n";
        result += "Default Value: " + defaultValue + "\n";
        return result;
    }
}

/// <summary>
/// url回复参数
/// </summary>
[Serializable]
public class UrlResPara
{
    public string para_name;
    public string para_type;
    public string para_details;
    public bool is_must;

    public override string ToString()
    {
        string result = "Parameter Name: " + para_name + "\n";
        result += "Parameter Type: " + para_type + "\n";
        result += "Parameter Details: " + para_details + "\n";
        result += "Is Required: " + (is_must ? "Yes" : "No") + "\n";
        return result;
    }
}

/// <summary>
/// 参数数据类型
/// </summary>
[Serializable]
public class DataType
{
    public RangeType range_type;
    public string type;
    public List<string> range_arr;

    public override string ToString()
    {
        string result = "DataType: " + type + "\n";
        result += "RangeType: " + range_type.ToString() + "\n";
        result += "RangeArray: ";
        if (range_arr != null)
        {
            foreach (string s in range_arr)
            {
                result += s + ",";
            }
            result = result.TrimEnd(',') + "\n";
        }
        else
        {
            result += "null\n";
        }
        return result;
    }
}

/// <summary>
/// 数据范围类型
/// </summary>
public enum RangeType
{
    ChooseOneMore,//多选1
    RangeOptional,//范围任选
}





