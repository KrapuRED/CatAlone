using UnityEngine;

public class CatHand : MonoBehaviour
{
    [Header("Events")]
    [SerializeField] private MovePawsToMouseEventSO _movePawsToMpuse;

    private void ShowPanel()
    {
        Debug.Log($"{gameObject.name} position: {transform.position.x}");
        Debug.Log("[CatHand - ShowPanel] paws and mouse are in right : " + IsOnRight());
        HoldMouseManager.instance.StartPhase(IsOnRight());
    }

    private bool IsOnRight()
    {
        return transform.position.x > 0;
    }

    private void MovePawToMouse(Transform mousePosition)
    {
        Debug.Log("[CatHand - MovePawToMouse] Moving toward Mouse");
        transform.position = mousePosition.position;
        ShowPanel();
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
