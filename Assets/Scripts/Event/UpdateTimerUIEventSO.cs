using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "UpdateTimerUIEventSO", menuName = "Scriptable Objects/UpdateTimerUIEventSO")]
public class UpdateTimerUIEventSO : ScriptableObject
{
    public UnityAction<float> OnUpdateTimerUI;

    public void Raise(float currentTimer)
    {
        OnUpdateTimerUI?.Invoke(currentTimer);
    }

    public void Register(UnityAction<float> listener)
    {
        OnUpdateTimerUI += listener;
    }

    public void Unregister(UnityAction<float> listener)
    {
        OnUpdateTimerUI -= listener;
    }
}
