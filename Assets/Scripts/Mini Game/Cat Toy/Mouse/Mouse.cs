using UnityEngine;

public class Mouse : MonoBehaviour
{
    [SerializeField] private Transform catchCheck;
    [SerializeField] private float radiuseCatchChcek;
    [SerializeField] private LayerMask catchLayer;

    [Header("Timing Config")]
    [SerializeField] private float _currentTime;
    [SerializeField] private float _catchTime;

    [Header("Word")]
    [SerializeField] private MouseWord _mouseWord;
    [SerializeField] protected MouseMovement _mouseMovement;
    [SerializeField] private bool _mouseCatch;
    public bool mouseCatch => _mouseCatch;  

    [Header("Events")]
    [SerializeField] private ActiveMouseWordEventSO _activeMouseWordEventSO;

    public MouseMovement MouseMovement => _mouseMovement;
    public float currentTime => _currentTime;
    public float catchTime => _catchTime;

    private void Start()
    {
        _activeMouseWordEventSO.OnRiase(_mouseWord);
    }

    public void UpdateTime()
    {
        if (!_mouseCatch)
            _currentTime += Time.deltaTime;
    }

    public void MoveToTarget(Vector3 targetWayPoint)
    {
        transform.position = targetWayPoint;
    }

    public void OpenWindowCatch()
    {
        Debug.Log("[IsInCatchLayer] : " + IsInCatchLayer());

        if (!_mouseCatch && IsInCatchLayer())
        {
            Debug.Log("[Opening Mouse to Get Catch]");
            _mouseWord.SetCurrentWord();
            _mouseWord.ShowWord();
        }
    }

    public void ReachEndPoint()
    {
        PlayCatchManager.instance.RemoveMouse(_mouseWord);
        Destroy(gameObject);
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

    public void ResetMouse()
    {
        _currentTime = 0;
    }

    public bool IsInCatchLayer()
    {
        return Physics2D.OverlapCircle(catchCheck.position, radiuseCatchChcek, catchLayer);
    }
}
