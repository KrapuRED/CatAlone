using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "EndBattleEventSO", menuName = "Scriptable Objects/EndBattleEventSO")]
public class EndBattleEventSO : ScriptableObject
{
    public UnityEvent EndBattle;

    public void OnRaiseEvent()
    {
        EndBattle?.Invoke();
    }

    public void Register(UnityAction listener)
    {
        EndBattle.AddListener(listener);
    }

    public void Unregister(UnityAction listener)
    {
        EndBattle.RemoveListener(listener);
    }
}
