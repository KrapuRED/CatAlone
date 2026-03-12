using UnityEngine;
using TMPro;

public class ObjectTyper : MonoBehaviour
{
    //SO for object word data
    [SerializeField] private ObjectTyperSO ObjectTyperData;
    public string word => ObjectTyperData.wordLetter;
    [SerializeField] private TextMeshProUGUI wordTextUI;
    [SerializeField] private string soundName;
    [SerializeField] private bool isCompleted;
    [SerializeField] private string _remeaningLetter;
    private int _charIndex;

    private void Start()
    {
        SetWordText(ObjectTyperData.wordLetter);
    }

    private void SetWordText(string wordLetter)
    {
        _remeaningLetter = wordLetter;
        wordTextUI.text = wordLetter;
    }

    public void CheckEnterLetter(string typingLetter)
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

    private bool IsCorrectLetter(string letter)
    {
        //Debug.Log($"Check Letter : {letter}");
        if (_charIndex >= ObjectTyperData.wordLetter.Length)
            return false;

        if (ObjectTyperData.wordLetter[_charIndex] == letter[_charIndex])
        {
            _charIndex++;
            return true;
        }

        return false;
    }

    private bool IsWordCompleted()
    {
        return _remeaningLetter.Length == 0;
    }

    public void RemoveLetter()
    {
        _remeaningLetter = _remeaningLetter.Remove(0, 1);
        SetWordText(_remeaningLetter);
    }

    public void ResetLetter()
    {
        SetWordText(ObjectTyperData.wordLetter);
    }

    public void OnWordCompleted()
    {
        //Debug.Log($"{ObjectTyperData.wordLetter} is complete!");
        _charIndex = 0;
        isCompleted = true;
        ResetLetter();
        AudioManager.instance.PlaySoundEffect(soundName);
        //LevelManager to change the scene and start to play the minigame
        if (ObjectTyperData.isCanChangeScene)
            LevelManager.instance.ChangeScene(ObjectTyperData.nameObject);
    }
}
