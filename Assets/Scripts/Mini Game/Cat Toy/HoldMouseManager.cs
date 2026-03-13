using UnityEngine;
using UnityEngine.InputSystem.XR;

public class HoldMouseManager : MiniGame
{
    public static HoldMouseManager instance;

    [Header("Hold Mouse Config")]
    [SerializeField] private float _gripZoneSize;
    [SerializeField] private float _maxGrip;
    [SerializeField] private float _minGrip;
    [SerializeField] private float _currentPositionStatus;
    [SerializeField] private float _currentLosePositionTime;
    [SerializeField] private float _releaseGripTimer;

    [Header("IsCanProgress Config")]
    [SerializeField] private int _completedProgress;
    [SerializeField] private int _gainProgressen;
    [SerializeField] private int _currentProgress;
    [SerializeField] private float _delayTime;
    [SerializeField] private float _currentGainTime;
    [SerializeField] private float _currentLoseProgressTime;

    [Header("Word")]
    [SerializeField] private WordBank _wordBank;
    [SerializeField] private string _currentWord = string.Empty;
    [SerializeField] private string _remainingWord = string.Empty;
    [SerializeField] private int _charIndex;

    [Header("UI")]
    [SerializeField] private HoldBarControllerUI _controller;
    [SerializeField] private UpdateGripTextEventSO _updateGripTextEventSO;
    [SerializeField] private bool _isPhase2Active;


    private void Awake()
    {
        if (instance == null) 
            instance = this;
        else
            Destroy(instance);
    }

    private void Update()
    {
        if (!_isPhase2Active)
            return;

        if (IsCanProgress())
        {
            _currentGainTime += Time.deltaTime;

            if (_currentGainTime >= _delayTime)
            {
                if (_currentProgress >= _completedProgress)
                {
                    EndMiniGame();
                    return;
                }

                _currentProgress += _gainProgressen;
                AudioManager.instance.PlaySoundEffect("Progress");

                _controller.UpdaetProgressBar(_currentProgress);
                _currentGainTime = 0;
            }
        }
        ReducePosition();
        ReduceProgress();
    }

    private void ReducePosition()
    {
        _currentLosePositionTime += Time.deltaTime;
        if (_currentLosePositionTime >= _delayTime)
        {
            if (_currentPositionStatus > 0)
            {
                _currentPositionStatus--;
                _currentLosePositionTime = 0;
                _controller.UpdateGribBar(_currentPositionStatus);
            }
        }
    }

    private void ReduceProgress()
    {
        _currentLoseProgressTime += Time.deltaTime;

        if (_currentLoseProgressTime >= _delayTime)
        {
            if (_currentProgress > 0)
            {
                _currentProgress--;
                _currentLoseProgressTime = 0;
                AudioManager.instance.PlaySoundEffect("Squeak");
                _controller.UpdaetProgressBar(_currentProgress);
            }

            if (_currentLoseProgressTime >= _releaseGripTimer)
                EndMiniGame();
        }
    }

    public void StartPhase(bool isRight)
    {
        _isPhase2Active = true;

        GenerateGripZone();

        float randomGrib = Random.Range(_minGrip/100, _maxGrip / 100);
        _controller.ShowBar(isRight, randomGrib);
        SetCurrentWord();
    }

    public override void CheckEnterLetter(string typingLetter)
    {
        if (_isPhase2Active)
        {
            Debug.Log("[HoldMouseManager - CheckEnterLetter] Is Correct Letter : " + IsCorrectLetter(typingLetter));
            if (IsCorrectLetter(typingLetter))
            {
                RemoveLetter();

                if (IsWordCompleted())
                    OnWordCompleted();
            }
            else
                ResetWord();
        }

    }

    private void SetCurrentWord()
    {
        _charIndex = 0;
        _currentWord = _wordBank.GetWordPlayer();
        SetRemainingWord(_currentWord);
    }

    private void SetRemainingWord(string wordLetter)
    {
        _remainingWord = wordLetter;
        //UI
        _updateGripTextEventSO.OnRaise(wordLetter);
    }

    private bool IsCorrectLetter(string letter)
    {
        if (letter == _remainingWord[_charIndex].ToString())
            return true;

        return false;
    }

    private bool IsWordCompleted()
    {
        return _remainingWord.Length == 0;
    }

    private void OnWordCompleted()
    {
        if (_isPhase2Active)
        {
            _currentPositionStatus += 10;
            _controller.UpdateGribBar(_currentPositionStatus);
            SetCurrentWord();
        }
    }

    private bool IsCanProgress()
    {
        if (_currentPositionStatus >= _minGrip && _currentPositionStatus <= _maxGrip)
        {
            return true;
        }
        return false;
    }

    private void RemoveLetter()
    {
        string remainingLetter = _remainingWord.Remove(0, 1);
        ManagerTyping.instance.ResetTyping();
        SetRemainingWord(remainingLetter);
    }

    private GameResult FindWinner()
    {
        if (_currentProgress >= _completedProgress)
            return GameResult.Win;

        if (_currentProgress <= 0)
            return GameResult.Loose;

        return GameResult.Loose;
    }

    private void GenerateGripZone()
    {
        _minGrip = Random.Range(0f, 100f - _gripZoneSize);
        _maxGrip = _minGrip + _gripZoneSize;

    }

    private void EndMiniGame()
    {
        MiniGameManager.instance.EndMiniGame(type, FindWinner());
    }

    private void ResetWord()
    {
        _charIndex = 0;
        SetRemainingWord(_currentWord);
        ManagerTyping.instance.ResetTyping();
    }
}
