using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance { get; private set; }

    [System.Serializable]
    public class UIPanel
    {
        public string name;
        public GameObject panel;
    }

    [Header("UI Panels")]
    public List<UIPanel> panels;

    private Dictionary<string, GameObject> panelDictionary;

    private void Awake()
    {
        // �̱��� �ʱ�ȭ
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);

        // �г� �ʱ�ȭ
        InitializePanels();
    }

    private void InitializePanels()
    {
        panelDictionary = new Dictionary<string, GameObject>();
        foreach (var panel in panels)
        {
            if (panel.panel != null)
            {
                panelDictionary.Add(panel.name, panel.panel);
                panel.panel.SetActive(false); // �ʱ� ��Ȱ��ȭ
            }
        }
    }

    // �г� Ȱ��ȭ
    public void ShowPanel(string panelName)
    {
        if (panelDictionary.TryGetValue(panelName, out GameObject panel))
        {
            panel.SetActive(true);
        }
        else
        {
            Debug.LogWarning($"Panel '{panelName}' not found!");
        }
    }

    // �г� ��Ȱ��ȭ
    public void HidePanel(string panelName)
    {
        if (panelDictionary.TryGetValue(panelName, out GameObject panel))
        {
            panel.SetActive(false);
        }
        else
        {
            Debug.LogWarning($"Panel '{panelName}' not found!");
        }
    }

    // ��� �г� ��Ȱ��ȭ
    public void HideAllPanels()
    {
        foreach (var panel in panelDictionary.Values)
        {
            panel.SetActive(false);
        }
    }
}
