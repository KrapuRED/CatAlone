using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "StartBattleEventSO", menuName = "Events/StartBattleEventSO")]
public class StartBattleEventSO : ScriptableObject
{
    public UnityEvent StartBattle;

    public void OnRaiseEvent()
    {
        StartBattle?.Invoke();
    }

    public void Register(UnityAction listener)
    {
        StartBattle.AddListener(listener);
    }

    public void Unregister(UnityAction listener)
    {
        StartBattle.RemoveListener(listener);
    }
}
