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
        MyNet.instance.AddNode(LocalUrlDataPool.Instance.urlDatas[0], (SuccessResult result) =>
        {
            LogExtension.LogSuccess("成功");
        }, (FailResult result) =>
        {
            LogExtension.LogFail("失败");
        });
    }
}
