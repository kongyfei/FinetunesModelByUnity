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
        // ��ȡ MyUrlDataSet �����ʵ��
        MyUrlDataSet myUrlDataSet = Resources.Load<MyUrlDataSet>("MyUrlDataSet");

        // ��ȡ MyUrlDataSet �����е� myUrlDatas �б�
        List<LocalUrlData> urlDatas = myUrlDataSet.myUrlDatas;

        MyNet.instance.AddNode(urlDatas[0], (SuccessResult result) =>
        {
            LogExtension.LogSuccess("�ɹ�");
        }, (FailResult result) =>
        {
            LogExtension.LogFail("ʧ��");
        });
    }
}
