using UnityEngine;
using System.Collections.Generic;
using TMPro;

public enum StatusScore
{
    None,
    Low,
    Balance,
    High
}

[System.Serializable]
public class EndingReward
{
    public StatusType statusType;
    public StatusScore statusScore;
    public string rewardText;
}

public class PanelEnding : MonoBehaviour
{
    [SerializeField] private List<EndingReward> endingRewards = new List<EndingReward>();

    [SerializeField] private TextMeshProUGUI hungerRewardText;
    [SerializeField] private TextMeshProUGUI socialRewardText;
    [SerializeField] private TextMeshProUGUI happinessReward;


    private void Start()
    {
        ShowEnding();
    }

    public void ShowEnding()
    {
        // Get status values from StatusManager
        float hunger = StatusManager.instance.GetHunger();
        float social = StatusManager.instance.GetSocial();
        float happiness = StatusManager.instance.GetHappiness();

        // Convert value -> score
        StatusScore hungerScore = GetStatusScore(hunger);
        StatusScore socialScore = GetStatusScore(social);
        StatusScore happinessScore = GetStatusScore(happiness);

        // Get reward text
        string hungerReward = SetRewardByStatusScore(StatusType.Hunger, hungerScore);
        string socialReward = SetRewardByStatusScore(StatusType.Social, socialScore);
        string happinessRewardText = SetRewardByStatusScore(StatusType.Happiness, happinessScore);

        // Show text
        hungerRewardText.text = hungerReward;
        socialRewardText.text = socialReward;
        happinessReward.text = happinessRewardText;
    }

    private StatusScore GetStatusScore(float value)
    {
        if (value >= 100)
            return StatusScore.High;

        if (value > 26 && value < 100)
            return StatusScore.Balance;
        
        return StatusScore.Low;
    }

    private string SetRewardByStatusScore(StatusType statusType, StatusScore statusScore)
    {
        foreach (var reward in endingRewards)
        {
            if (reward.statusType == statusType && reward.statusScore == statusScore)
            {
                return reward.rewardText;
            }
        }

        return string.Empty;
    }
}
