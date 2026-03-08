using System.Collections.Generic;
using UnityEngine;

public class ManagerTyping : MonoBehaviour
{
    public static ManagerTyping instance;

    public List<ObjectTyper> allObjects = new List<ObjectTyper>();
    [SerializeField] private string currentLetter = "";
    [SerializeField] private ObjectTyper lockTarget;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }

    public void EnterTypeLetter(string typedLetter)
    {
        currentLetter += typedLetter;
        Debug.Log("Letter : " + currentLetter);

        if (lockTarget != null)
        {
            CheckLockTarget();
            return;
        }

        List<ObjectTyper> matches = new List<ObjectTyper>();

        foreach (var obj  in allObjects)
        {
            if (obj.word.StartsWith(currentLetter))
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
        if (currentLetter.Length > lockTarget.word.Length || !lockTarget.word.StartsWith(currentLetter))
        {
            ResetTyping();
            return;
        }

        if (lockTarget.word == currentLetter)
        {
            lockTarget.OnWordCompleted();

            ResetTyping();
        }
    }

    private void ResetTyping()
    {
        currentLetter = "";
        lockTarget = null;
    }
}
