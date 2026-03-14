using UnityEngine;
using System.Collections.Generic;

public class StatusBarController : MonoBehaviour
{
    [SerializeField] private List<StatusBarUI> statusBarUIs = new List<StatusBarUI>();

    [SerializeField] private UpdateStatusUIEventSO UpdateStatusUI;

    private void Start()
    {
        StatusManager.instance.RefreshUI();
    }

    private  void UpdateStatus(float valueHungger, float valueSocial, float valueHappnes)
    {
       foreach (StatusBarUI statusBar in statusBarUIs)
        {
            if (statusBar.statusData.statusType == StatusType.Hunger)
                statusBar.UpdateStatusBar(valueHungger);

            if (statusBar.statusData.statusType == StatusType.Social)
                statusBar.UpdateStatusBar(valueSocial);

            if (statusBar.statusData.statusType == StatusType.Happiness)
                statusBar.UpdateStatusBar(valueHappnes);
        }
        Debug.Log($"[StatusBarController] Hunger: {valueHungger} Social: {valueSocial} Happiness: {valueHappnes}");
    }

    private void OnEnable()
    {
        UpdateStatusUI.Register(UpdateStatus);
    }

    private void OnDisable()
    {
        UpdateStatusUI.Unregister(UpdateStatus);
    }
}
