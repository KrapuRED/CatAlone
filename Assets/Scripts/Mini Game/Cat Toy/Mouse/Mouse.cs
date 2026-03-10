using UnityEngine;

public class Mouse : MonoBehaviour
{
    [Header("Timing Config")]
    [SerializeField] private float _currentTime;
    [SerializeField] private float _catchTime;

    private bool _isOnRight;

    // Update is called once per frame
    void Update()
    {
        
    }

    public bool IsCanCatchMouse()
    {
        return false;
    }

    public void MouseGetCatch()
    {

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
}
