using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using LocalData;

/// <summary>
/// ����ͨ�Ų��Խű�
/// </summary>
public class MyNetTest : MonoBehaviour
{
    [Header("���԰�ť")]
    public Button testBtn;

    public void Test()
    {
        MyNet.instance.AddNode(LocalUrlDataPool.Instance.urlDatas[0], (SuccessResult result) =>
        {
            LogExtension.LogSuccess("�ɹ�");
        }, (FailResult result) =>
        {
            LogExtension.LogFail("ʧ��");
        });
    }
}
