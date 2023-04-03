using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using LocalData;

/// <summary>
/// 网络通信测试脚本
/// </summary>
public class MyNetTest : MonoBehaviour
{
    [Header("测试按钮")]
    public Button testBtn;

    public void Test()
    {
        // 获取 MyUrlDataSet 对象的实例
        MyUrlDataSet myUrlDataSet = Resources.Load<MyUrlDataSet>("MyUrlDataSet");

        // 获取 MyUrlDataSet 对象中的 myUrlDatas 列表
        List<LocalUrlData> urlDatas = myUrlDataSet.myUrlDatas;

        MyNet.instance.StartAsycnNet(urlDatas[0], (object result) =>
        {
            LogExtension.LogSuccess("成功");
        }, (string result) =>
        {
            LogExtension.LogFail("失败");
        });
    }
}
