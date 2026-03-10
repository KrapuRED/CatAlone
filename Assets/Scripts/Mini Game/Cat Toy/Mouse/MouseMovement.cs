using UnityEngine;

public class MouseMovement : Mouse
{
    [Header("WayPoint")]
    [SerializeField] protected Transform StartWayPoint;
    [SerializeField] protected Transform EndWayPoint;

    [SerializeField] private float movementSpeed;

    private bool _isResting;
    private Rigidbody2D rb2d;

    private void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        MoveToTarget(EndWayPoint);
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
