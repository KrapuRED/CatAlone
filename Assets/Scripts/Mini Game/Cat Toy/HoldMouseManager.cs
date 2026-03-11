using UnityEngine;
using UnityEngine.InputSystem.XR;

public class HoldMouseManager : MiniGame
{
    public static HoldMouseManager instance;

    [Header("Hold Mouse Config")]
    [SerializeField] private float _highesRangeOutside;
    [SerializeField] private float _lowesRangeOutside;
    [SerializeField] private float _currentPositionStatus;

    [Header("Progress Config")]
    [SerializeField] private float _completedProgress;
    [SerializeField] private float _gainProgressen;
    [SerializeField] private float _currentProgress;

    [Header("Word")]
    [SerializeField] private WordBank _wordBank;
    [SerializeField] private string _currentWord = string.Empty;
    [SerializeField] private string _remainingWord = string.Empty;
    [SerializeField] private int _charIndex;

    [Header("UI")]
    [SerializeField] private HoldBarControllerUI _controller;

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
        
    }

    public void StartPhase(bool isRight)
    {
        _isPhase2Active = true;
        _controller.ShowBar(isRight);
        SetCurrentWord();
    }

    public override void CheckEnterLetter(string typingLetter)
    {
        if (IsCorrectLetter(typingLetter) && _isPhase2Active)
        {
            _charIndex++;
            RemoveLetter();

            if (IsWordCompleted())
                OnWordCompleted();
        }
        else
            ResetWord();

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
            Progress();
            SetCurrentWord();
        }
    }

    private void Progress()
    {
        if (_currentPositionStatus >= _lowesRangeOutside ||  _currentPositionStatus <= _highesRangeOutside)
        {
            _currentProgress += _gainProgressen;
        }
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

        return GameResult.Loose;
    }

    private void ResetWord()
    {
        _charIndex = 0;
        SetRemainingWord(_currentWord);
        ManagerTyping.instance.ResetTyping();
    }
}
