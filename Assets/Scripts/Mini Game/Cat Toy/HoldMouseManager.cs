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

    [SerializeField] private float _gripDepleteTimer;
    [SerializeField] private float _gripDepleteDelay = 0.5f; // faster depletion

    [SerializeField] private float _releaseGripTimer;

    [Header("Progress Config")]
    [SerializeField] private int _completedProgress = 100;
    [SerializeField] private int _gainProgressen = 4;
    [SerializeField] private int _currentProgress;

    [SerializeField] private float _progressGainTimer;
    [SerializeField] private float _progressLoseTimer;

    [SerializeField] private float _progressGainDelay = 1f;
    [SerializeField] private float _progressLoseDelay = 1f;

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
            Destroy(gameObject);
    }

    private void Update()
    {
        if (!_isPhase2Active)
            return;

        ReduceGrip();
        HandleProgress();
    }

    // -------------------------
    // GRIP SYSTEM
    // -------------------------

    private void ReduceGrip()
    {
        _gripDepleteTimer += Time.deltaTime;

        if (_gripDepleteTimer >= _gripDepleteDelay)
        {
            _currentPositionStatus -= 1f;

            _currentPositionStatus = Mathf.Clamp(_currentPositionStatus, 0f, 100f);

            _controller.UpdateGribBar(_currentPositionStatus);

            _gripDepleteTimer = 0;
        }
    }

    // -------------------------
    // PROGRESS SYSTEM
    // -------------------------

    private void HandleProgress()
    {
        if (IsCanProgress())
        {
            _progressGainTimer += Time.deltaTime;

            if (_progressGainTimer >= _progressGainDelay)
            {
                if (_currentProgress >= _completedProgress)
                {
                    EndMiniGame();
                    return;
                }

                _currentProgress += _gainProgressen;
                _currentProgress = Mathf.Clamp(_currentProgress, 0, _completedProgress);

                _controller.UpdaetProgressBar(_currentProgress);

                AudioManager.instance.PlaySoundEffect("Progress");

                _progressGainTimer = 0;
            }
        }
        else
        {
            _progressLoseTimer += Time.deltaTime;

            if (_progressLoseTimer >= _progressLoseDelay)
            {
                if (_currentProgress > 0)
                {
                    _currentProgress--;
                    _controller.UpdaetProgressBar(_currentProgress);

                    AudioManager.instance.PlaySoundEffect("Squeak");
                }

                _progressLoseTimer = 0;
            }
        }
    }

    // -------------------------
    // START PHASE
    // -------------------------

    public void StartPhase(bool isRight)
    {
        _isPhase2Active = true;

        GenerateGripZone();

        float zoneCenter = (_minGrip + _maxGrip) / 2f / 100f;

        _controller.ShowBar(isRight, zoneCenter);

        SetCurrentWord();
    }

    // -------------------------
    // TYPING SYSTEM (UNCHANGED)
    // -------------------------

    public override void CheckEnterLetter(string typingLetter)
    {
        if (_isPhase2Active)
        {
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

            _currentPositionStatus = Mathf.Clamp(_currentPositionStatus, 0f, 100f);

            _controller.UpdateGribBar(_currentPositionStatus);

            SetCurrentWord();
        }
    }

    // -------------------------
    // GRIP ZONE CHECK
    // -------------------------

    private bool IsCanProgress()
    {
        return _currentPositionStatus >= _minGrip && _currentPositionStatus <= _maxGrip;
    }

    // -------------------------
    // WORD SYSTEM
    // -------------------------

    private void RemoveLetter()
    {
        string remainingLetter = _remainingWord.Remove(0, 1);
        ManagerTyping.instance.ResetTyping();
        SetRemainingWord(remainingLetter);
    }

    private void ResetWord()
    {
        _charIndex = 0;
        SetRemainingWord(_currentWord);
        ManagerTyping.instance.ResetTyping();
    }

    // -------------------------
    // END GAME
    // -------------------------

    private GameResult FindWinner()
    {
        if (_currentProgress >= _completedProgress)
            return GameResult.Win;

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
}