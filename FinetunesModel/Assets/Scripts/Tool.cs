using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tool
{
    public class DebugExtension
    {
        public static void LogFail(string content)
        {
            Debug.Log($"<color=#FF0000>{content}</color>");
        }

        public static void LogSuccess(string content)
        {
            Debug.Log($"<color=#00FF00>{content}</color>");
        }
    }
}
