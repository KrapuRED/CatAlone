using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "ReadyToTapEventSO", menuName = "Events/ReadyToTapEventSO")]
public class ReadyToTapEventSO : ScriptableObject
{
    public UnityAction<TapKey> OnReadyToTap;

    public void OnRaise(TapKey tapKey)
    {
        OnReadyToTap?.Invoke(tapKey);
    }

    public void Register(UnityAction<TapKey> listener)
    {
        OnReadyToTap += listener;
    }
    public void Unregister(UnityAction<TapKey> listener)
    {
        OnReadyToTap -= listener;
    }
}
