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
        // 싱글톤 초기화
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

        // 패널 초기화
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
                panel.panel.SetActive(false); // 초기 비활성화
            }
        }
    }

    // 패널 활성화
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

    // 패널 비활성화
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

    // 모든 패널 비활성화
    public void HideAllPanels()
    {
        foreach (var panel in panelDictionary.Values)
        {
            panel.SetActive(false);
        }
    }
}
