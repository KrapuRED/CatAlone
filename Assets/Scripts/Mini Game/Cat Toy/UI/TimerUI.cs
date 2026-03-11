using TMPro;
using UnityEngine;

public class TimerUI : MonoBehaviour
{
    public TextMeshProUGUI timerValue;
    public TextMeshProUGUI timerText;

    public void SetCurrentTimer(float timer)
    {
        timerValue.text = Mathf.Ceil(timer).ToString();
    }
}
