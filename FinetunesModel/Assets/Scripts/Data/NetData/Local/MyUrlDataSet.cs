using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "MyUrlDataSet", menuName = "CreateMyUrlDataSet")]
public class MyUrlDataSet : ScriptableObject
{
    [SerializeField]
    private List<MyUrlData> myUrlDatas;
}
