using System.Collections;
using UnityEngine;

public class MouseMovement : MonoBehaviour
{
    [SerializeField] private Mouse mouse;

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

    public void MoveToTarget(Transform waypointTarget)
    {
        if (_isResting)
            return;

        RotateToTarget(waypointTarget);
        float distance = Vector2.Distance(transform.position, waypointTarget.position);

        if (distance < 0.1f)
        {
            //stop
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
        yield return new WaitForSeconds(4f);
        _isResting = false;
    }

    private void RotateToTarget(Transform waypoint)
    {
        Vector2 Direction = waypoint.position - transform.position;

        float angle = Mathf.Atan2(Direction.y, Direction.x) * Mathf.Rad2Deg;

        rb2d.rotation = angle;
    }

    private void OnWalking(Transform waypoint)
    {
        rb2d.MovePosition(
            Vector2.MoveTowards(
                rb2d.position,
                waypoint.position,
                movementSpeed * Time.deltaTime
            ));
    }
}
