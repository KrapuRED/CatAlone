using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "CheckLetterEventSO", menuName = "Scriptable Objects/CheckLetterEventSO")]
public class CheckLetterEventSO : ScriptableObject
{
    public UnityAction<string> OnCheckLetter;

    public void OnRiasee(string letter)
    {
        OnCheckLetter?.Invoke(letter);
    }

    public void Register(UnityAction<string> listener)
    {
        OnCheckLetter += listener;
    }

    public void Unregister(UnityAction<string> listener)
    {
        OnCheckLetter += listener;
    }
}
