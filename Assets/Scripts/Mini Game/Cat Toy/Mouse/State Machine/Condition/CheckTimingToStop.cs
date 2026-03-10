using UnityEngine;

public class CheckTimingToStop : Condition
{
    [SerializeField] private Mouse _currentMouse;
    [SerializeField] private float timeToStop;
    
    public override bool CheckCondition()
    {
        if (_currentMouse.currentTime >= timeToStop)
            return true;

        return false;
    }
}
