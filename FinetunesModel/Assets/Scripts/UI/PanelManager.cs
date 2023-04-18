using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ��������
/// </summary>
public class PanelManager : MonoSingleton<PanelManager>
{
    /// <summary>
    /// ����������ʾ˳��
    /// </summary>
    private Stack<PanelBase> panels;

    /// <summary>
    /// ������Ż�
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
                LogExtension.LogFail("�������޷��˳�");
            }
        }
        else
        {
            LogExtension.LogFail($"���˳�����,ջ����壺{curPanel.gameObject.name}����ǰ�ر����{panelBase.gameObject.name}");
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
            LogExtension.LogFail("������ҳ��˳����ʾ����");
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

        LogExtension.LogFail($"������{name}���");
        return null;
    }
}
