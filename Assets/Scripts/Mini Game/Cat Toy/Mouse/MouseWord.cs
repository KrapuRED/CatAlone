using TMPro;
using UnityEngine;

public class MouseWord : MonoBehaviour
{
    [SerializeField] private WordBank _wordBank;
    [SerializeField] private GameObject _wordUI;
    [SerializeField] private TextMeshPro _wordText;
    [SerializeField] private string _currentWord;
    [SerializeField] private int _currentCharIndex;

    public string currentWord => _currentWord;

    public void SetCurrentWord()
    {
        _currentWord = _wordBank.GetWordPlayer();
    }

    public void ShowWord()
    {
        _wordText.text = _currentWord;
        _wordUI.SetActive(true);
    }

    public void HideWord()
    {
        _wordText.text = string.Empty;
        _currentWord = string.Empty;
        _wordUI.SetActive(false);
    }

    public Mouse GetMouse()
    {
        HideWord();
        return GetComponentInParent<Mouse>();
    }

    public bool IsCorrectLetter(string letter)
    {
        if (_currentCharIndex >= _currentWord.Length) 
            return false;

        if (letter == _currentWord[_currentCharIndex].ToString())
        {
            _currentCharIndex++;
            ManagerTyping.instance.ResetTyping();
            return true;
        }

        _currentCharIndex = 0;
        PlayCatchManager.instance.ResetWord();
        return false;
    }

    public void SetRemainingWord(string remainingWord)
    {
        _wordText.text = remainingWord;
    }
    
}
