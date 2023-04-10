using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 面板管理类
/// </summary>
public class PanelManager : MonoSingleton<PanelManager>
{
    private Stack<PanelBase> panels;
    private Dictionary<string, GameObject> prefabPool;

    public Transform UIParent;
    public string rootPath;

    protected override void Awake()
    {
        base.Awake();
          
        panels = new Stack<PanelBase>();
        prefabPool = new Dictionary<string, GameObject>();
    }

    public void Show<T>()
    { 
        string typeName = typeof(T).ToString();
        GameObject prefab;
        if (!prefabPool.ContainsKey(typeName))
        {
            string prefabPath = rootPath + typeName;
            prefab = Resources.Load(prefabPath) as GameObject;
            prefabPool.Add(typeName, prefab);
        }
        else
        {
            prefab = prefabPool[typeName];
        }

        GameObject panel = Instantiate(prefab, Vector3.zero, Quaternion.identity, UIParent);
        PanelBase panelBase = panel.GetComponent<PanelBase>();
        panels.Push(panelBase);

        panelBase.Init();
        panelBase.Show();
    }
}
