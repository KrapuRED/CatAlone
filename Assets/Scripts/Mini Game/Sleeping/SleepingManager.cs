using UnityEngine;
using System.Collections.Generic;

public class SleepingManager : MiniGame
{
    public static SleepingManager instance;

    [Header("Duration Sleep Config")]
    [SerializeField] private List<MiniGameDuration> sleepDurations = new List<MiniGameDuration>();
    [SerializeField] private float _sleepDuration;
    [SerializeField] private float _currentSleepTime;
    [SerializeField] protected bool _isSleeping;
    [SerializeField] private TimerUI _timerUI;

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
        {
            _currentSleepTime -= Time.deltaTime;
            _timerUI.SetCurrentTimer(_currentSleepTime);
        }

        if (_currentSleepTime <= 0 && _isSleeping)
            WeakingUp();

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

        foreach (MiniGameDuration sleep in sleepDurations)
        {
            cumulative += sleep.change;

            if (randomValue < cumulative)
            {
                _sleepDuration = _currentSleepTime = sleep.Duration;
                _isSleeping = true;
                AudioManager.instance.PlaySoundEffect("Purring");
                return;
            }
        }
    }

    private void WeakingUp()
    {
        _isSleeping = false;
        AudioManager.instance.StopSoundEffect();
        AudioManager.instance.PlaySoundEffect("Pop");
        MiniGameManager.instance.EndMiniGame(type, GameResult.Win);
    }
}
