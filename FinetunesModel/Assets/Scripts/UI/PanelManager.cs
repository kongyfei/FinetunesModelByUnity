using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 面板管理类
/// </summary>
public class PanelManager : MonoSingleton<PanelManager>
{
    /// <summary>
    /// 管理界面的显示顺序
    /// </summary>
    private Stack<PanelBase> panels;

    /// <summary>
    /// 缓存池优化
    /// </summary>
    private Dictionary<string, GameObject> prefabPool;
    private Dictionary<string, GameObject> panelPool;

    public Transform UIParent;
    public string releasePath;
    public string testPath;

    private List<PrefabAsset> prefabAssets;

    protected override void Awake()
    {
        base.Awake();

        panels = new Stack<PanelBase>();
        prefabPool = new Dictionary<string, GameObject>();
        panelPool = new Dictionary<string, GameObject>();
    }

    private void Start()
    {
        PrefabAssets tempPrefabAssets = Resources.Load<PrefabAssets>("Data/MyPrefabAssets");
        prefabAssets = tempPrefabAssets.myPrefabAssets;
    }

    public void Show<T>()
    {
        string typeName = typeof(T).ToString();
        PrefabAsset curPanelAsset = GetPrefabAssetByName(typeName);

        if (curPanelAsset.type == PanelType.Interface)
        {
            while (panels.Count > 0 && panels.Peek().type == PanelType.Popups)
            {
                GameObject gameObject = panels.Pop().gameObject;
                Destroy(gameObject);
            }

            if (panels.Count > 0)
            {
                PanelBase hidePanel = panels.Peek();
                hidePanel.gameObject.SetActive(false);
            }
        }
        else if (curPanelAsset.type == PanelType.Popups)
        {
            if (panels.Count > 0)
            {
                PanelBase hidePanel = panels.Peek();
                hidePanel.GetComponent<CanvasGroup>().blocksRaycasts = false;
            }
        }

        int popCount = 0;
        bool isPop = false;
        foreach (var item in panels)
        {
            string name = item.gameObject.name;
            if (name == typeName)
            {
                isPop = true;
                break;
            }
            popCount++;
        }

        PanelBase panelBase;
        if (isPop)
        {
            for (int i = 0; i < popCount; i++)
            {
                GameObject gameObject = panels.Pop().gameObject;
                Destroy(gameObject);
            }

            panelBase = panels.Peek();
            panelBase.gameObject.SetActive(true);
        }
        else
        {
            GameObject prefab;
            if (!prefabPool.ContainsKey(typeName))
            {
                string prefabPath = releasePath + curPanelAsset.path;
                prefab = Resources.Load(prefabPath) as GameObject;
                prefabPool.Add(typeName, prefab);
            }
            else
            {
                prefab = prefabPool[typeName];
            }

            GameObject panel = Instantiate(prefab, UIParent, false);
            panel.name = typeName;
            panelBase = panel.GetComponent<PanelBase>();
            panelBase.name = typeName;
            panels.Push(panelBase);
            panelBase.state = PanelState.Load;
        }
        panelBase.GetComponent<CanvasGroup>().blocksRaycasts = true;
        panelBase.OnInit();
        panelBase.OnShow();
        panelBase.state = PanelState.Show;
    }

    public void Hide(PanelBase panelBase)
    {
        PanelBase curPanel = panels.Peek();
        if (curPanel.gameObject.name == panelBase.gameObject.name)
        {
            if (!curPanel.main)
            {
                panelBase.OnHide();
                panels.Pop();
                Destroy(panelBase.gameObject);

                if (panels.Count > 0)
                {
                    PanelBase showPanel = panels.Peek();
                    showPanel.gameObject.SetActive(true);
                    showPanel.GetComponent<CanvasGroup>().blocksRaycasts = true;
                }
            }
            else
            {
                LogExtension.LogFail("主界面无法退出");
            }
        }
        else
        {
            LogExtension.LogFail($"面板顺序错误,栈顶面板：{curPanel.gameObject.name}，当前关闭面板{panelBase.gameObject.name}");
        }
    }

    public void ShowLoading()
    {
        Show<LoadingPanel>();
    }

    public void HideLoading()
    {
        PanelBase loading = panels.Peek();
        if (loading.name == "LoadingPanel")
        {
            Hide(loading);
        }
        else
        {
            LogExtension.LogFail("加载中页面顺序显示错误");
        }
    }

    private PrefabAsset GetPrefabAssetByName(string name)
    {
        for (int i = 0; i < prefabAssets.Count; i++)
        {
            if (name == prefabAssets[i].name)
            {
                return prefabAssets[i];
            }
        }

        LogExtension.LogFail($"不存在{name}面板");
        return null;
    }
}
