using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "UpdateTutorialEventSO", menuName = "Scriptable Objects/UpdateTutorialEventSO")]
public class UpdateTutorialEventSO : ScriptableObject
{
    public UnityEvent OnUpdateTutorial;

    public void OnRaise()
    {
        Debug.Log("[UpdateTutorialEventSO - OnUpdateTutorial]");
        OnUpdateTutorial?.Invoke();
    }

    public void Register(UnityAction listener)
    {
        OnUpdateTutorial.AddListener(listener);
    }

    public void Unregister(UnityAction listener)
    {
        OnUpdateTutorial.RemoveListener(listener);
    }
}
