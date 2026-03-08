using UnityEngine;

public class MeowBattleManager : MiniGame
{
    public static MeowBattleManager instance;

    [Header("Score")]
    [SerializeField] private int _gainScoreNeighbourCat;
    [SerializeField] private int _gainScorePlayerCat;
    [SerializeField] private int _neighbourCatScore;
    [SerializeField] private int _playerCatScore;

    [SerializeField] private WordBank _wordBank;
    //newString bank
    [SerializeField] private string _currentWord = "";
    //remeaning meow
    [SerializeField] private string _remainingWord = string.Empty;

    [SerializeField] private WordTypingUI _wordTypingUI;

    [Header("Events")]
    [SerializeField] private StartBattleEventSO _startBattleEventSO;
    [SerializeField] private EndBattleEventSO _endBattleEventSO;

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
    }

    public override void CheckEnterLetter(string typingLetter)
    {
        Debug.Log("[MeowBattleManager] Letter : " + typingLetter);
        Debug.Log(IsCorrectLetter(typingLetter));
        if (IsCorrectLetter(typingLetter))
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

    private void SetCurrentWord()
    {
        _currentWord = _wordBank.GetWord();
        SetRemainingWord(_currentWord);
        _startBattleEventSO.OnRaiseEvent();
    }

    private bool IsCorrectLetter(string letters)
    {
        //Debug.Log($"Check Letter : {letters}");
        return _currentWord.StartsWith(letters);
    }

    private void SetRemainingWord(string newString)
    {
        if (_remainingWord == null)
        {
            Debug.LogWarning("Word Typing UI is MISSING!");
            return;
        }
        _remainingWord = newString;
        _wordTypingUI.SetWordtyping(_remainingWord);
    }

    private void RemoveLetter()
    {
        string newString = _remainingWord.Remove(0, 1);
        SetRemainingWord(newString);
    }

    private bool IsWordCompleted()
    {
        return _remainingWord.Length == 0;
    }

    public void OnWordCompleted()
    {
        Debug.Log($"{_currentWord} is completed!");

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
        if (_playerCatScore < 5 || _neighbourCatScore < 5)
        {
            Debug.Log($"Mini Game are DONE!" +
                $"with score {_playerCatScore} : {_neighbourCatScore}");
            _endBattleEventSO.OnRaiseEvent();
        }
    }

    private void ResetTyping()
    {
        SetRemainingWord(_currentWord);

        ManagerTyping.instance.ResetTyping();
    }
}
