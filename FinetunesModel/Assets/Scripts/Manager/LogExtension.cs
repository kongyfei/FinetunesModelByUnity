using System;
using UnityEngine;

/// <summary>
/// ��־��ӡ��չ
/// ע����չ����չ�����ͱ����ǿ���ʵ�����Ķ��󣬷����޷�����
/// </summary>
public class LogExtension
{
    public static void LogFail(string message)
    {
        Debug.Log("<color=red>" + message + "</color>");
    }

    public static void LogSuccess(string message)
    {
        Debug.Log("<color=green>" + message + "</color>");
    }
}
