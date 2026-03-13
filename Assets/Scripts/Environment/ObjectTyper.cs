using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ObjectTyper : MonoBehaviour
{
    //SO for object word data
    [SerializeField] private ObjectTyperSO ObjectTyperData;
    public string word => _currentLetter;
    [SerializeField] private TextMeshProUGUI wordTextUI;
    [SerializeField] private string soundName;
    [SerializeField] private string _currentLetter;
    [SerializeField] private string _remeaningLetter;
    [SerializeField] private int _charIndex;

    [SerializeField] private List<GameObject> hiddenObjects = new List<GameObject>();
    [SerializeField] private bool isContainer;
    [SerializeField] private Sprite closeSprite;
    [SerializeField] private Sprite OpenSprite;
    [SerializeField] private bool isOpened;

    [SerializeField] private SpriteRenderer _spriteRenderer;

    private void Start()
    {
        _currentLetter = ObjectTyperData.wordLetter;
        SetWordText(_currentLetter);

        if (isContainer)
        {
            _spriteRenderer.sprite = closeSprite;
            foreach (GameObject obj in hiddenObjects)
            {
                obj.SetActive(false);
            }
        }
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
        //Debug.Log($"Check Letter : {currentLetter}");
        if (_charIndex >= _currentLetter.Length || string.IsNullOrEmpty(letter) || _charIndex >= letter.Length)
            return false;


        if (_currentLetter[_charIndex] == letter[_charIndex])
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
        _charIndex = 0;
        SetWordText(ObjectTyperData.wordLetter);
    }

    private void ToggleHiddenObjects()
    {
        isOpened = !isOpened;

        if (isOpened)
        {
            _currentLetter = "close";
            _spriteRenderer.sprite = OpenSprite;
            SetWordText(_currentLetter);
        }
        else
        {
            _currentLetter = "open";
            _spriteRenderer.sprite = closeSprite;
            SetWordText(_currentLetter);
        }

        foreach (var obj in hiddenObjects)
        {
            obj.SetActive(isOpened);
        }
    }

    public void OnWordCompleted()
    {
        //Debug.Log($"{ObjectTyperData.wordLetter} is complete!");
        _charIndex = 0;
        ResetLetter();
        ManagerTyping.instance.ResetTyping();
        AudioManager.instance.PlaySoundEffect(soundName);

        if (isContainer)
        {
            ToggleHiddenObjects();
            return;
        }


        //LevelManager to change the scene and start to play the minigame
        if (ObjectTyperData.isCanChangeScene)
            LevelManager.instance.ChangeScene(ObjectTyperData.nameObject);
    }
}
