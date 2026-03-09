using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "RemoveTapKeyEventSO", menuName = "Events/RemoveTapKeyEventSO")]
public class RemoveTapKeyEventSO : ScriptableObject
{
    public UnityAction<TapKey> OnRemoveTapKey;

    public void OnRaise(TapKey tapKey)
    {
        OnRemoveTapKey?.Invoke(tapKey);
    }

    public void Register(UnityAction<TapKey> listener)
    {
        OnRemoveTapKey += listener;
    }
    public void Unregister(UnityAction<TapKey> listener)
    {
        OnRemoveTapKey -= listener;
    }
}

