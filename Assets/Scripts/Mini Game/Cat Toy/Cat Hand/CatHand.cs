using UnityEngine;

public class CatHand : MonoBehaviour
{
    [SerializeField] private bool _isOnRight;

    [Header("Events")]
    [SerializeField] private MovePawsToMouseEventSO _movePawsToMpuse;

    private void ShowPanel()
    {
        Debug.Log("[CatHand - ShowPanel] paws and mouse are in right : " + IsOnRight());
    }

    private bool IsOnRight()
    {
        if (transform.position.x > 0)
        {
            _isOnRight = true;

        }
        else
            _isOnRight = false;

        return _isOnRight;
    }

    private void MovePawToMouse(Transform mousePosition)
    {
        Debug.Log("[CatHand - MovePawToMouse] Moving toward Mouse");
        ShowPanel();
        transform.position = mousePosition.position;
    }

    private void OnEnable()
    {
        _movePawsToMpuse.Register(MovePawToMouse);
    }

    private void OnDisable()
    {
        _movePawsToMpuse.Unegister(MovePawToMouse);
    }
}
