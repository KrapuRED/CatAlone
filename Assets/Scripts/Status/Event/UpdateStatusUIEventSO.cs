using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "UpdateStatusUIEventSO", menuName = "Events/UpdateStatusUIEventSO")]
public class UpdateStatusUIEventSO : ScriptableObject
{
    public UnityAction<float, float, float> OnUpdateStatusUI;

    public void OnRaise(float valueHungger, float valueSocial, float valueHappnes)
    {
        OnUpdateStatusUI?.Invoke(valueHungger, valueSocial, valueHappnes);
    }

    public void Register(UnityAction<float, float, float> listener)
    {
        OnUpdateStatusUI += listener;
    }

    public void Unregister(UnityAction<float, float, float> listener)
    {
        OnUpdateStatusUI -= listener;
    }
}
