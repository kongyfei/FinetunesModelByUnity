using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "MyPrefabAssets", menuName = "CreateMyPrefabAssets")]
public class PrefabAssets : ScriptableObject
{
    [SerializeField]
    public List<PrefabAsset> myPrefabAssets;    
}

