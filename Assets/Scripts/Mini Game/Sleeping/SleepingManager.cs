using UnityEngine;
using System.Collections.Generic;

[System.Serializable]
public class SleepDuration
{
    public float Duration;
    public int change;
}

public class SleepingManager : MiniGame
{
    public static SleepingManager instance;

    [Header("Duration Sleep Config")]
    [SerializeField] private List<SleepDuration> sleepDurations = new List<SleepDuration>();
    [SerializeField] private float _sleepDuration;
    [SerializeField] private float _currentSleepTime;
    [SerializeField] protected bool _isSleeping;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }

    private void Update()
    {
        if (_currentSleepTime > 0)
            _currentSleepTime -= Time.deltaTime;

        if (_currentSleepTime <= 0 && _isSleeping)
            MiniGameManager.instance.EndMiniGame(type, GameResult.Win);
    }

    private void Start()
    {
        StartMiniGame();
    }

    public override void CheckEnterLetter(string typingLetter)
    {
        ManagerTyping.instance.ResetTyping();
    }

    public void StartMiniGame()
    {
        int randomValue = Random.Range(0, 100);

        int cumulative = 0;

        foreach (SleepDuration sleep in sleepDurations)
        {
            cumulative += sleep.change;

            if (randomValue < cumulative)
            {
                _sleepDuration = _currentSleepTime = sleep.Duration;
                _isSleeping = true;
                return;
            }
        }
    }
}
