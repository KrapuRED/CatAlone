using UnityEngine;

public class MeowBattleManager : MiniGame
{
    public static MeowBattleManager instance;

    [Header("Score")]
    [SerializeField] private int _gainScoreNeighbourCat;
    [SerializeField] private int _gainScorePlayerCat;
    [SerializeField] private int _neighbourCatScore;
    [SerializeField] private int _playerCatScore;

    [Header("Word")]
    [SerializeField] private WordBank _wordBank;
    //newString bank
    [SerializeField] private string _currentWord = "";
    //remeaning meow
    [SerializeField] private string _remainingWord = string.Empty;
    [SerializeField] private int _charIndex;

    [Header("UI")]
    [SerializeField] private WordTypingUI _wordTypingUI;
    [SerializeField] ControllerStatusMeowBarUI _controllerStatusMeowBarUI;

    [Header("Events")]
    [SerializeField] private StartBattleEventSO _startBattleEventSO;
    [SerializeField] private EndBattleEventSO _endBattleEventSO;

    [SerializeField] private bool _isBattleOnGoing;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }

    private void Start()
    {
        SetCurrentWord();
        CheckScore();
        _isBattleOnGoing = true;
    }

    public override void CheckEnterLetter(string typingLetter)
    {
        Debug.Log("[MeowBattleManager] Letter : " + typingLetter);
        bool isCorrectLetter = IsCorrectLetter(typingLetter);
        Debug.Log(isCorrectLetter);
        if (_isBattleOnGoing)
        {
            if (isCorrectLetter)
            {
                RemoveLetter();

                if (IsWordCompleted())
                {
                    OnWordCompleted();
                }
            }
            else
            {
                ResetTyping();
            }
        }  
    }

    private void SetCurrentWord()
    {
        _currentWord = _wordBank.GetWordPlayer();
        _charIndex = 0;
        SetRemainingWord(_currentWord);
        _startBattleEventSO.OnRaiseEvent();
    }

    private bool IsCorrectLetter(string letter)
    {
        //Debug.Log($"Check Letter : {letter}");
        if (_charIndex >= _currentWord.Length)
            return false;

        if (_currentWord[_charIndex].ToString() == letter)
        {
            _charIndex++;
            return true;
        }
            
        return false;
    }

    private void SetRemainingWord(string newString)
    {
        if (_wordTypingUI == null)
        {
            Debug.LogWarning("Word Typing UI is MISSING!");
            return;
        }

        _remainingWord = newString;
        _wordTypingUI.SetWordtyping(_remainingWord);
    }

    private void RemoveLetter()
    {
        Debug.Log("RemoveLetter");
        string newString = _remainingWord.Remove(0, 1);
        ManagerTyping.instance.ResetTyping();
        SetRemainingWord(newString);
    }

    private bool IsWordCompleted()
    {
        return _remainingWord.Length == 0;
    }

    public void OnWordCompleted()
    {
        _playerCatScore += _gainScorePlayerCat;
        _neighbourCatScore -= _gainScorePlayerCat;
        CheckScore();

        SetCurrentWord();
        ResetTyping() ;
    }

    public void NeighbourCatMeowing()
    {
        _playerCatScore -= _gainScoreNeighbourCat;
        _neighbourCatScore += _gainScoreNeighbourCat;
        CheckScore();
    }

    private void CheckScore()
    {
        _controllerStatusMeowBarUI.CalculateMeowBar(_playerCatScore, _neighbourCatScore);

        //Check Score for win conditiion
        if (_playerCatScore <= 5 || _neighbourCatScore <= 5 && _isBattleOnGoing)
        {
            GameResult result = FindWinner();
            
            _isBattleOnGoing = false;

            MiniGameManager.instance.EndMiniGame(type, result);
            _endBattleEventSO.OnRaiseEvent();
        }
    }

    private GameResult FindWinner()
    {
        if (_playerCatScore <= 0)
            return GameResult.Loose;

        return GameResult.Loose;
    }

    private void ResetTyping()
    {
        SetRemainingWord(_currentWord);
        _charIndex = 0;
        ManagerTyping.instance.ResetTyping();
    }
}
