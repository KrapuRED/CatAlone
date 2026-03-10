using UnityEngine;

public class CheckTimingToStop : Condition
{
    [SerializeField] private Mouse _currentMouse;
    
    public override bool CheckCondition()
    {
        return false;
    }
}
