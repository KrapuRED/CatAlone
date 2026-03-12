using TMPro;
using UnityEngine;

public class Tutorial : MonoBehaviour
{
    [SerializeField] private string currentLetter;
    [SerializeField] private string remainingLetter;
    [SerializeField] private int _charIndex;
    [SerializeField] private TextMeshProUGUI tutorialLetter;

    public void CheckLetter(string typingLetter)
    {
        bool isCorrectLetter = IsCorrectLetter(typingLetter);
        Debug.Log(isCorrectLetter);
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
            ResetLetter();
        }
    }

    private void Start()
    {
        ActiveTutorial();
    }

    private void ActiveTutorial()
    {
        remainingLetter = currentLetter;
        _charIndex = 0;
    }

    private void SetWordText(string letter)
    {
        tutorialLetter.text = letter;
    }

    private bool IsCorrectLetter(string typedWord)
    {
        //Debug.Log($"Check Letter : {currentLetter}");
        if (_charIndex >= typedWord.Length)
            return false;

        return currentLetter.StartsWith(typedWord);
    }

    private bool IsWordCompleted()
    {
        return remainingLetter.Length == 0;
    }

    public void RemoveLetter()
    {
        remainingLetter = remainingLetter.Remove(0, 1);
        SetWordText(remainingLetter);
    }

    public void ResetLetter()
    {
        _charIndex = 0;
        remainingLetter = currentLetter;
        ManagerTyping.instance.ResetTyping();
        SetWordText(currentLetter);
    }

    public void OnWordCompleted()
    {
        //remove from tutorial list
        ManagerTyping.instance.ResetTyping();
        TutorialManager.instance.NextTutorial(this);
        gameObject.SetActive(false);
    }
}
