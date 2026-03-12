using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StatusBarUI : MonoBehaviour
{
    public StatusSO statusData;
    public TextMeshProUGUI statusNameText;
    public Slider statusbarMeter;

    private void Start()
    {
        statusNameText.text = statusData.statusName;
       
    }

    public void UpdateStatusBar(float valueStatus)
    {
        statusbarMeter.value = valueStatus / 100;
    }
}
