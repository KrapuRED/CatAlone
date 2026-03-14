using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SmackFeederManager : MiniGame
{
    public static SmackFeederManager instance;

    [Header("Smack Feeder Config")]
    [SerializeField] private float _spawnRateTap;
    [SerializeField] private float _lengthMiniGame;
    [SerializeField] private int _maxMissClick;
    [SerializeField] private int _missClick;
    [SerializeField] private List<TapKey> _tapKeys = new List<TapKey>();
    [SerializeField] private bool _isFeedingDone;

    [Header("Timing Config")]
    [SerializeField] private float _currentTiming;
    [SerializeField] private float _currentLengthMiniGame;
    [SerializeField] private List<MiniGameDuration> durationMiniGames = new List<MiniGameDuration>();
    [SerializeField] private List<SpawnRate> spawnRates = new List<SpawnRate>();

    [Header("Helper")]
    [SerializeField] private TapKeyWord _tapKeyWord;
    [SerializeField] private GenerateTapKey _generateTapKey;

    [Header("Events")]
    [SerializeField] private ReadyToTapEventSO _readyToTapEventSO;
    [SerializeField] private RemoveTapKeyEventSO _removeTapKeyEventSO;

    [SerializeField] private FeederControllerUI _controllerUI;
    private bool _isMiniGameActive;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }

    private void Start()
    {
        StartMiniGame();
    }

    public void StartMiniGame()
    {
        SetSpawnRate();
        SetDurationMiniGame();
    }

    private void SetDurationMiniGame()
    {
        int randomValue = Random.Range(0, 100);

        int cumulative = 0;

        foreach (MiniGameDuration durationMiniGame in durationMiniGames)
        {
            cumulative += durationMiniGame.change;

            if (randomValue < cumulative)
            {
                _lengthMiniGame = _currentLengthMiniGame = durationMiniGame.Duration;
                _isMiniGameActive = true;
                SetCurrentKeyWord();
                return;
            }
        }
    }

    private void SetSpawnRate()
    {
        int randomValue = Random.Range(0, 100);

        int cumulative = 0;

        foreach (SpawnRate spawnRate in spawnRates)
        {
            cumulative += spawnRate.rate;

            if (randomValue < cumulative)
            {
                _spawnRateTap = spawnRate.spawnRate;
                return;
            }
        }
    }

    private void Update()
    {
       if (_isMiniGameActive)
        {
            _currentTiming += Time.deltaTime;
            _currentLengthMiniGame -= Time.deltaTime;
        }

        if (_missClick >= _maxMissClick)
        {
            EndMiniGame();
            return;
        }

        if (_currentLengthMiniGame <= 0)
        {
            EndMiniGame();
        }
    }

    private GameResult FindWinner()
    {
        if (_missClick == _maxMissClick)
            return GameResult.Loose;
        return GameResult.Win;
    } 

    public override void CheckEnterLetter(string typingLetter)
    {
        if (_currentLengthMiniGame < 0)
            return;

        TapKey currentKey = _tapKeys.First();

        if (currentKey == null)
        {
            _currentTiming = 0;
            return;
        }

        if (!currentKey.IsCorrectKey(typingLetter.ToUpper()))
        {
            _missClick++;
            _controllerUI.UpdateFailedUI();
            if (_missClick >= _maxMissClick)
                EndMiniGame();
            MissRemoveTapKey(currentKey);
            currentKey.OnDestroyKey();
            return;
        }

        //check _targetTiming on the first pop-up
        string result = currentKey.CheckTiming(_currentTiming);
       
        AudioManager.instance.PlaySoundEffect(result);

        //if in perfect timig say "Perfect" or miss say "MISS"
        ManagerTyping.instance.ResetTyping();
        RemoveTapKey(currentKey);
        currentKey.OnDestroyKey();
    }

    private void SetCurrentKeyWord()
    {
        string newKey = _tapKeyWord.GetKeyWord();

        _generateTapKey.OnGenerateTapKey(newKey);

        StartCoroutine(CoolDownSpawnRate());
    }

    private IEnumerator CoolDownSpawnRate()
    {
        yield return new WaitForSeconds(_spawnRateTap);
        SetCurrentKeyWord();
    }

    private void EndMiniGame()
    {
        StopAllCoroutines();
        //Say to status win or lose
        //AudioManager.instance.StopSoundEffect();
        _isMiniGameActive = false;
       
        _isFeedingDone = true;  
        MiniGameManager.instance.EndMiniGame(type, FindWinner());
    }

    private void AssignTapKey(TapKey tapKey)
    {
        _tapKeys.Add(tapKey);
    }

    private void MissRemoveTapKey(TapKey tapKey)
    {
        _missClick++;
        _currentTiming = 0;
        _controllerUI.UpdateFailedUI();
        ManagerTyping.instance.ResetTyping();

        _tapKeys.Remove(tapKey);
    }

    private void RemoveTapKey(TapKey tapKey)
    {
        _currentTiming = 0;
        ManagerTyping.instance.ResetTyping();

        _tapKeys.Remove(tapKey);
    }

    private void OnEnable()
    {
        _readyToTapEventSO.Register(AssignTapKey);
        _removeTapKeyEventSO.Register(MissRemoveTapKey);
    }

    private void OnDisable()
    {
        _readyToTapEventSO.Unregister(AssignTapKey);
        _removeTapKeyEventSO.Unregister(MissRemoveTapKey);
    }
}
