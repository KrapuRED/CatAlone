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

    [Header("UI")]
    [SerializeField] private WordTypingUI _wordTypingUI;
    [SerializeField] ControllerStatusMeowBarUI _controllerStatusMeowBarUI;

    [Header("Events")]
    [SerializeField] private StartBattleEventSO _startBattleEventSO;
    [SerializeField] private EndBattleEventSO _endBattleEventSO;

    private bool _isBattleDone;

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
        _isBattleDone = true;
    }

    public override void CheckEnterLetter(string typingLetter)
    {
      /*Debug.Log("[MeowBattleManager] Letter : " + typingLetter);
        Debug.Log(IsCorrectLetter(typingLetter));*/
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
        _currentWord = _wordBank.GetWordPlayer();
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
        if (_playerCatScore <= 5 || _neighbourCatScore <= 5 && _isBattleDone)
        {
            string winner = FindWinner();
            string panel = string.Empty;

            Debug.Log(winner);
            if (winner != null && winner == "player")
                panel = "winning";
            else
                panel = "lose";

            _isBattleDone = false;

            ManagerPanel.instance.OpenPanel(panel);
            _endBattleEventSO.OnRaiseEvent();
        }
    }

    private string FindWinner()
    {
        if (_playerCatScore <= 0)
            return "neighbour";
        
        if (_neighbourCatScore <= 0)
            return "player";

        return "";
    }

    private void ResetTyping()
    {
        SetRemainingWord(_currentWord);

        ManagerTyping.instance.ResetTyping();
    }
}
