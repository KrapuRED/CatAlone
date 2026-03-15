using TMPro;
using UnityEngine;

public class Panel : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI rewardUI;
    [SerializeField] private UpdateRewardEventSO updateRewardEventSO;

    private void UpdateRewardUI(MiniGameType miniGameType ,float rewardValue)
    {
        switch (miniGameType)
        {
            case MiniGameType.CatToy:
                rewardUI.text = $"gain {rewardValue.ToString()} Fun";
                break;

            case MiniGameType.BattleMeow:
                rewardUI.text = $"gain {rewardValue.ToString()} Social";
                break;

            case MiniGameType.Feeding:
                rewardUI.text = $"gain {rewardValue.ToString()} Hunger";
                break;

            case MiniGameType.None:
                break;
        }
    }

    private void OnEnable()
    {
        updateRewardEventSO.Register(UpdateRewardUI);
    }

    private void OnDisable()
    {
        updateRewardEventSO.Unregister(UpdateRewardUI);
    }
}
