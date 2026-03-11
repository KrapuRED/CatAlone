using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "UpdateGripTextEventSO", menuName = "Events/UpdateGripTextEventSO")]
public class UpdateGripTextEventSO : ScriptableObject
{
    public UnityAction<string> UpdateGripText;

    public void OnRaise(string letter)
    {
        UpdateGripText?.Invoke(letter);
    }

    public void Register(UnityAction<string> lintener)
    {
        UpdateGripText += lintener;
    }

    public void Unregister(UnityAction<string> lintener)
    {
        UpdateGripText -= lintener;
    }
}
