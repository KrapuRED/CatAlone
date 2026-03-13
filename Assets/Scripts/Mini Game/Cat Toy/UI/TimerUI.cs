using TMPro;
using UnityEngine;

public class TimerUI : MonoBehaviour
{
    public TextMeshProUGUI timerValue;
    public TextMeshProUGUI timerText;

    [SerializeField] private UpdateTimerUIEventSO updateTimerUI;

    public void SetCurrentTimer(float timer)
    {
        timerValue.text = Mathf.Ceil(timer).ToString();
    }

    private void OnEnable()
    {
        updateTimerUI.Register(SetCurrentTimer);
    }

    private void OnDisable()
    {
        updateTimerUI.Unregister(SetCurrentTimer);
    }
}
