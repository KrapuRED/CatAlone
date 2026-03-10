using UnityEngine;

public class Mouse : MonoBehaviour
{
    [Header("Timing Config")]
    [SerializeField] private float _currentTime;
    [SerializeField] private float _catchTime;

    [Header("Word")]
    [SerializeField] private MouseWord _mouseWord;
    [SerializeField] protected MouseMovement _mouseMovement;
    [SerializeField] private bool _mouseCatch;

    [Header("Events")]
    [SerializeField] private ActiveMouseWordEventSO _activeMouseWordEventSO;

    public MouseMovement MouseMovement => _mouseMovement;
    public float currentTime => _currentTime;
    public float catchTime => _catchTime;

    private bool _isOnRight;

    private void Start()
    {
        _activeMouseWordEventSO.OnRiase(_mouseWord);
    }

    public void UpdateTime()
    {
        _currentTime += Time.deltaTime;
    }

    public void MoveToTarget(Vector3 targetWayPoint)
    {
        transform.position = targetWayPoint;
    }

    public void OpenWindowCatch()
    {
        if (!_mouseCatch)
        {
            Debug.Log("[Opening Mouse to Get Catch]");
            _mouseWord.SetCurrentWord();
            _mouseWord.ShowWord();
        }
    }

    public void CloseWindowCatch()
    {
        Debug.Log("[Closing Mouse to Get Catch]");
        _mouseWord.HideWord();
    }

    public void MouseGetCatch()
    {
        _mouseCatch = true;
    }

    public bool IsOnRight()
    {
        if (transform.position.x > 0)
        {
            _isOnRight = true;

        }
        else
            _isOnRight = false;

        Debug.Log("Mouse on the right = " + _isOnRight);
        return _isOnRight;
    }

    public void ResetMouse()
    {
        _currentTime = 0;
    }
}
