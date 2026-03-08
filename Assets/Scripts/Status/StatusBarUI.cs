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
        statusbarMeter.value = statusData.deafultStatusPoint;
    }
}
