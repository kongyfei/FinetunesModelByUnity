using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LocalData;

[CreateAssetMenu(fileName = "MyUrlDataSet", menuName = "CreateMyUrlDataSet")]
public class MyUrlDataSet : ScriptableObject
{
    [SerializeField]
    public List<LocalUrlData> myUrlDatas;
}
