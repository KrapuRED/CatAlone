using UnityEngine;

public class FailedMarkUI : MonoBehaviour
{
    public GameObject failedIcon;
    [SerializeField] private bool _isFail;
    public bool isFail => _isFail;

    public void UpdateFailUI()
    {
        if (_isFail)
            return;

        _isFail = true;
        failedIcon.SetActive(true);
    }
}
