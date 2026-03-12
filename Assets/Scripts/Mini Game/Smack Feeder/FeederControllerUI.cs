using UnityEngine;
using System.Collections.Generic;

public class FeederControllerUI : ControllerUI
{
    [SerializeField] private List<FailedMarkUI> FailedMarks= new List<FailedMarkUI>();

    public void UpdateFailedUI()
    {
        Debug.Log("[FeederControllerUI - UpdateFailUI]");
        foreach (var mark in FailedMarks)
        {
            if (!mark.isFail)
            {
                mark.UpdateFailUI();
                break;
            }
        }
    }
}
