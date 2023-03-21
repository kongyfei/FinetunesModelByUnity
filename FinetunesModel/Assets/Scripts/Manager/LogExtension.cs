using System;
using UnityEngine;

/// <summary>
/// 日志打印扩展
/// 注：扩展函扩展的类型必须是可以实例化的对象，否则无法访问
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
