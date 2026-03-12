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

    [Header("Timing Config")]
    [SerializeField] private float _currentTiming;
    [SerializeField] private float _currentLengthMiniGame;

    [Header("Helper")]
    [SerializeField] private TapKeyWord _tapKeyWord;
    [SerializeField] private GenerateTapKey _generateTapKey;

    [Header("Events")]
    [SerializeField] private ReadyToTapEventSO _readyToTapEventSO;
    [SerializeField] private RemoveTapKeyEventSO _removeTapKeyEventSO;

    [SerializeField] private FeederControllerUI _controllerUI;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }

    private void Start()
    {
        SetCurrentKeyWord();
        _currentLengthMiniGame = _lengthMiniGame;
    }

    private void Update()
    {
        _currentTiming += Time.deltaTime;
        _currentLengthMiniGame -= Time.deltaTime;

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
        if (_missClick >= _maxMissClick)
            return GameResult.Win;
        return GameResult.Loose;
    } 

    public override void CheckEnterLetter(string typingLetter)
    {
        if (_currentLengthMiniGame < 0)
            return;

        TapKey currentKey = _tapKeys.First();
        if (!currentKey.IsCorrectKey(typingLetter.ToUpper()))
        {
            _missClick++;
            _controllerUI.UpdateFailedUI();
            if (_missClick >= _maxMissClick)
                EndMiniGame();
            return;
        }

        //check _targetTiming on the first pop-up
        string result = currentKey.CheckTiming(_currentTiming);
       
        Debug.Log(result);

        //if in perfect timig say "Perfect" or miss say "MISS"
        ManagerTyping.instance.ResetTyping();
        RemoveTapKey(currentKey);
        currentKey.OnCorrectKey();
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
