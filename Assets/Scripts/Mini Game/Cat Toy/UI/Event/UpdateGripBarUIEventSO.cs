using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "UpdateGripBarUIEventSO", menuName = "Events/UpdateGripBarUIEventSO")]
public class UpdateGripBarUIEventSO : ScriptableObject
{
    public UnityAction<float> UpdateGripBarUI;

    public void OnRiase(float gripValue)
    {
        UpdateGripBarUI?.Invoke(gripValue);
    }

    public void Register(UnityAction<float> listener)
    {
        UpdateGripBarUI += listener;
    }

    public void Unegister(UnityAction<float> listener)
    {
        UpdateGripBarUI -= listener;
    }
}
