using UnityEngine;

public class CheckTimingToMove : Condition
{
    [SerializeField] private Mouse _currentMouse;
    [SerializeField] private float timeToMove;

    public override bool CheckCondition()
    {
        if (_currentMouse.currentTime <= timeToMove)
        {
            return true;
        }
        return false;
    }
}
