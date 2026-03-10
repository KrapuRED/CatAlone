using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "ActiveMouseWordEventSO", menuName = "Events/ActiveMouseWordEventSO")]
public class ActiveMouseWordEventSO : ScriptableObject
{
    public UnityAction<MouseWord> OnActiveMouseWord;

    public void OnRiase(MouseWord activeMouseWord)
    {
        OnActiveMouseWord?.Invoke(activeMouseWord);
    }

    public void Register(UnityAction<MouseWord> listener)
    {
        OnActiveMouseWord += listener;
    }

    public void Unregister(UnityAction<MouseWord> listener)
    {
        OnActiveMouseWord -= listener;
    }
}
