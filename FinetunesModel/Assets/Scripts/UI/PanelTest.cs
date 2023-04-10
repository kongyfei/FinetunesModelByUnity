using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelTest : MonoBehaviour
{
    public void Test()
    {
        PanelManager.Instance.Show<TestPanel>();
    }
}
