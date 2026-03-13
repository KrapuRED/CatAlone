using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "UpdateRewardEventSO", menuName = "Scriptable Objects/UpdateRewardEventSO")]
public class UpdateRewardEventSO : ScriptableObject
{
    public UnityAction<MiniGameType, float> OnUpdateReward;

    public void OnRiase(MiniGameType miniGameType, float rewardValue)
    {
        OnUpdateReward?.Invoke(miniGameType, rewardValue);
    }

    public void Register(UnityAction<MiniGameType, float> listener)
    {
        OnUpdateReward += listener;
    }

    public void Unregister(UnityAction<MiniGameType, float> listener)
    {
        OnUpdateReward -= listener;
    }
}
