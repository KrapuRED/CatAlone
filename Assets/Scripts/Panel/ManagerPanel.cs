using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Panel
{
    public string panelName;
    public GameObject panel;
}

public class ManagerPanel : MonoBehaviour
{
    public static ManagerPanel instance;

    [Header("Panel Config")]
    public List<Panel> panelList = new List<Panel>();

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
