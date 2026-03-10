using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "MovePawsToMouseEventSO", menuName = "Events/MovePawsToMouseEventSO")]
public class MovePawsToMouseEventSO : ScriptableObject
{
    public UnityAction<Transform> OnMovePawsToMouse;

    public void OnRaise(Transform mousePosition)
    {
        OnMovePawsToMouse?.Invoke(mousePosition);
    }

    public void Register(UnityAction<Transform> listener)
    {
        OnMovePawsToMouse += listener;
    }

    public void Unegister(UnityAction<Transform> listener)
    {
        OnMovePawsToMouse -= listener;
    }
}
