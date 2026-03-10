using UnityEngine;
using System.Collections.Generic;

public class PlayCatchManager : MiniGame
{
    public static PlayCatchManager instance;

    [Header("Play Catch Config")]
    [SerializeField] private List<MouseWord> _activeMouseWords = new List<MouseWord>();
    [SerializeField] private string _currentWord = string.Empty;
    [SerializeField] private string _remainingWord = string.Empty;
    [SerializeField] private Mouse _mouseCatch;
    [SerializeField] private bool _isMouseCatch;

    [Header("Mouse Word")]
    [SerializeField] private MouseWord _lockMouseWord;

    [Header("Events")]
    [SerializeField] private ActiveMouseWordEventSO _activeMouseWordEventSO;
    [SerializeField] private MovePawsToMouseEventSO _movePawsToMouseEventSO;
    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }

    public override void CheckEnterLetter(string typingLetter)
    {
        //Debug.Log("[PlayCatchManager - CheckEnterLetter] check letter : " + typingLetter);
        if (_lockMouseWord != null)
        {
            CheckLockWord(typingLetter);
            return;
        }
            
        _lockMouseWord = FindMouseWord(typingLetter);

        if (IsWordCompleted())
            ManagerTyping.instance.ResetTyping();

        if (_lockMouseWord == null)
            return;

        _currentWord = _remainingWord = _lockMouseWord.currentWord;

        RemoveLetter();
    }

    private MouseWord FindMouseWord(string letter)
    {
        //Debug.Log("[PlayCatchManager - FindMouseWord] check letter : " + letter);
        if (_activeMouseWords.Count == 0) 
            return null;

        List<MouseWord> matches = new List<MouseWord>();

        foreach (MouseWord word in _activeMouseWords)
        {
            bool isCorrectLetter = word.IsCorrectLetter(letter);
            //Debug.Log("[PlayCatchManager - IsCorrectLetter] check letter : " + isCorrectLetter);
            if (isCorrectLetter)
            {
                matches.Add(word);
            }
                
        }
            
        if (matches.Count == 0)
            return null;

        if (matches.Count == 1)
            return matches[0];

        return null;
    }

    private void CheckLockWord(string letter)
    {
        bool isCorrectLetter = _lockMouseWord.IsCorrectLetter(letter);
        /*Debug.Log("[PlayCatchManager - CheckLockWord] check letter : " + letter);
        Debug.Log("[PlayCatchManager - CheckLockWord] check letter : " + isCorrectLetter);*/
        if (isCorrectLetter && !_isMouseCatch)
        {
            RemoveLetter();

            if (IsWordCompleted())
            {
                _isMouseCatch = true;
                _mouseCatch = _lockMouseWord.GetMouse();
                _mouseCatch.MouseGetCatch();
                _movePawsToMouseEventSO.OnRaise(_mouseCatch.transform);
                Debug.Log("Catch The Mouse!");
            }
        }
        else
        {
            ResetWord();
        }
    }

    private void RemoveLetter()
    {
        Debug.Log("[PlayCatchManager - RemoveLetter] Remove Letter!");
        if (!IsWordCompleted())
        {
            string newString = _remainingWord.Remove(0, 1);
            SetRemainingWord(newString);
        }
    }

    private void SetRemainingWord(string letter)
    {
        _remainingWord = letter;
        _lockMouseWord.SetRemainingWord(_remainingWord);
    }

    private bool IsWordCompleted()
    {
        return _remainingWord.Length == 0;
    }

    public void ResetWord()
    {
        _lockMouseWord = null;
        _remainingWord = _currentWord = string.Empty;
    }

    private void AssignToActiveMouseWords(MouseWord activeMouseWord)
    {
        _activeMouseWords.Add(activeMouseWord);
    }

    private void OnEnable()
    {
        _activeMouseWordEventSO.Register(AssignToActiveMouseWords);
    }

    private void OnDisable()
    {
        _activeMouseWordEventSO.Unregister(AssignToActiveMouseWords);
    }
}
