using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PanelData
{
    public string panelName;
    public GameObject panel;
}

public class ManagerPanel : MonoBehaviour
{
    public static ManagerPanel instance;

    [Header("PanelData Config")]
    public List<PanelData> panelList = new List<PanelData>();

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }

    public void OpenPanel(string panel)
    {
        for (int i = 0; i < panelList.Count; i++)
        {
            if (panelList[i].panelName == panel)
                panelList[i].panel.SetActive(true);
        }
    }
}
