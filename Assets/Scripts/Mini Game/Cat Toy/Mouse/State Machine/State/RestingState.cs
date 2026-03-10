using UnityEngine;

public class RestingState : State
{
   [SerializeField] private MouseMovement _mouseMovement;

    private void OnEnable()
    {
        StartCoroutine(_mouseMovement.RestRoaming());
    }

    void Update()
    {
        // allow state machine to switch again
        if (!_mouseMovement.isResting)
        {
            enabled = false; // exit this state
        }
    }
}
