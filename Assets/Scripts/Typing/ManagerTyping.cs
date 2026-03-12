using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class ManagerTyping : MonoBehaviour
{
    public static ManagerTyping instance;

    public List<ObjectTyper> allObjects = new List<ObjectTyper>();
    [SerializeField] private string typingLetter = "";
    [SerializeField] private ObjectTyper lockTarget;

    [SerializeField] private CheckLetterEventSO checkLetterEvent;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }

    public void EnterTypeLetter(string typedLetter)
    {
        typingLetter += typedLetter;
        Debug.Log("Letter : " + typingLetter);

        //check if the minigame is playing
        if (!LevelManager.instance.CheckHomeScene())
            CheckMiniGame(typingLetter);

        if (!GameManager.instance.isMiniGameActive)
            CheckObjectTypeLetter();
    }

    #region Home Scene Typing
    private void CheckObjectTypeLetter()
    {
        if (lockTarget != null)
        {
            CheckLockTarget();
            return;
        }

        if (allObjects.Count == 0)
            return;

        List<ObjectTyper> matches = new List<ObjectTyper>();

        foreach (var obj in allObjects)
        {
            if (obj.word.StartsWith(typingLetter))
                matches.Add(obj);
        }

        if (matches.Count == 0)
        {
            ResetTyping();
            return;
        }

        if (matches.Count == 1)
        {
            lockTarget = matches[0];
        }
    }

    private void CheckLockTarget()
    {
        if (typingLetter.Length > lockTarget.word.Length || !lockTarget.word.StartsWith(typingLetter))
        {
            ResetTyping();
            return;
        }

        if (lockTarget.word == typingLetter)
        {
            lockTarget.OnWordCompleted();

            ResetTyping();
        }
    }
    #endregion

    #region Neigbour Cat Typing
    private void CheckMiniGame(string typeLetter)
    {
        checkLetterEvent.OnRiasee(typeLetter);
    }
    #endregion

    public void ResetTyping()
    {
        typingLetter = "";
        lockTarget = null;
    }
}
