using UnityEngine;
using System.Collections.Generic;

[System.Serializable]
public class WayPoint
{
    public string waypointName;
    public bool isUp;
    public Transform wayPointPosition;
}

public class MouseSpawnManager : MonoBehaviour
{
    public static MouseSpawnManager instance;

    [Header("Mouse Spawner Config")]
    [SerializeField] private GameObject mousePrefab;
    [SerializeField] private Transform spawnContinaer;
    [SerializeField] private List<WayPoint> waypoints = new List<WayPoint>();
    [SerializeField] private float _spawnTime;
    [SerializeField] private bool _canSpawn;
    [SerializeField] private float _currentSpawnTime;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }

    private void Update()
    {
        if (!_canSpawn)
            return;

        // Only allow 1 mouse at a time
        if (spawnContinaer.childCount >= 1)
            return;

        _currentSpawnTime += Time.deltaTime;

        if (_currentSpawnTime >= _spawnTime)
        {
            _currentSpawnTime = 0;
            SpawnMouse();
        }
    }

    public void SpawnMouse()
    {
        int randomSpawnPos = Random.Range(0, waypoints.Count);

        WayPoint startPoint = waypoints[randomSpawnPos];
        WayPoint endPoint = GetEndWayPoint(randomSpawnPos);

        if (startPoint == null || endPoint == null)
        {
            Debug.LogWarning("[MouseSpawnManager - SpawnMouse] Missing Start and End Point are NULL!");
            return;
        }

        GameObject newMouse = Instantiate(mousePrefab, startPoint.wayPointPosition.position, Quaternion.identity, spawnContinaer);

        if (newMouse == null)
            return;

        Mouse mouse = newMouse.GetComponent<Mouse>();
        if (mouse != null)
        {
            mouse.MouseMovement.SetUpWayPoints(startPoint.wayPointPosition, endPoint.wayPointPosition);
        }
    }

    public void StopSpawn()
    {
        _canSpawn = false;
    }

    private WayPoint GetEndWayPoint(int indexWayPoint)
    {
        WayPoint startPoint = waypoints[indexWayPoint];

        List<WayPoint> possiblEndPoint = new List<WayPoint>();

        foreach (WayPoint waypoint in waypoints)
        {
            if (waypoint.isUp != startPoint.isUp)
            {
                possiblEndPoint.Add(waypoint);
            }
        }

        if (possiblEndPoint.Count <= 0)
            return null;

        int randomIndex = Random.Range(0, possiblEndPoint.Count);
        return possiblEndPoint[randomIndex];
    }
}