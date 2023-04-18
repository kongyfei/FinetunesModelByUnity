using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "RemoteTestData", menuName = "CreateMyRemoteTestData")]
public class TestData : ScriptableObject
{
    [SerializeField]
    public CompanyDataList companys;
}
