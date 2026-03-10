using UnityEngine;

public class PatrolState : State
{
    [SerializeField] private Mouse _currentMouse;

    // Update is called once per frame
    void Update()
    {
        if (!_currentMouse.MouseMovement.isResting)
        {
            _currentMouse.UpdateTime();
            _currentMouse.MouseMovement.OnRoaming();
        }
    }
}
