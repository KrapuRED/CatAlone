using System.Collections;
using UnityEngine;

public class MouseMovement : MonoBehaviour
{
    [SerializeField] private Mouse _currentMouse;

    [Header("WayPoint")]
    [SerializeField] protected Transform StartWayPoint;
    [SerializeField] protected Transform EndWayPoint;

    [SerializeField] private float movementSpeed;

    [SerializeField] private bool _isResting;
    private Rigidbody2D rb2d;

    public bool isResting => _isResting;

    private void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }

    public void SetUpWayPoints(Transform startPoint, Transform EndPoint)
    {
        StartWayPoint = startPoint;
        EndWayPoint = EndPoint;
    }

    public void MoveToTarget(Transform waypointTarget)
    {
        if (_isResting)
            return;

        RotateToTarget(waypointTarget);
        float distance = Vector2.Distance(transform.position, waypointTarget.position);

        if (distance < 0.1f)
        {
            //stop
            _currentMouse.ReachEndPoint();
            OnStopMovement();
            return;
        }

        OnWalking(waypointTarget);
    }

    public void OnRoaming()
    {
        MoveToTarget(EndWayPoint);
    }

    public void OnStopMovement()
    {
        rb2d.linearVelocity = Vector2.zero;
    }

    public IEnumerator RestRoaming()
    {
        if (_isResting)
        {
            yield break;
        }

        _isResting = true;

        _currentMouse.OpenWindowCatch();

        yield return new WaitForSeconds(_currentMouse.catchTime);

        _currentMouse.CloseWindowCatch();
        _currentMouse.ResetMouse();
        _isResting = false;
    }

    private void RotateToTarget(Transform waypoint)
    {
        Vector2 direction = waypoint.position - transform.position;

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        rb2d.rotation = angle - 90f;
    }

    private void OnWalking(Transform waypoint)
    {
        rb2d.MovePosition(
            Vector2.MoveTowards(
                rb2d.position,
                waypoint.position,
                movementSpeed * Time.deltaTime
            ));
        _currentMouse.MoveToTarget(transform.position);
    }
}
