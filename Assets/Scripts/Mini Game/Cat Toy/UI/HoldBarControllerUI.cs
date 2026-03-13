using UnityEngine;
using UnityEngine.UI;

public class HoldBarControllerUI : MonoBehaviour
{
    [Header("Game Object")]
    [SerializeField] private GripBarUI _leftBar;
    [SerializeField] private GripBarUI _rightBar;
    [SerializeField] private GameObject _progressBar;
    [SerializeField] private float _currentProgress;

    [Header("UI")]
    [SerializeField] private Image _sliderProgress;

    [Header("Event")]
    [SerializeField] private UpdateGripBarUIEventSO _updateGripBarUI;

    public void ShowBar(bool isRight, float middlePoint)
    {
        //Debug.Log("[HoldBarControllerUI - ShowBar] middle poinr : " + middlePoint);

        if (isRight)
            _leftBar.SetGribBarStatic(middlePoint);
        else
            _rightBar.SetGribBarStatic(middlePoint);

        _progressBar.SetActive(true);
    }

    public void HideBar()
    {
        //Fire Event
    }

    public void UpdaetProgressBar(float progress)
    {
        _sliderProgress.fillAmount = progress / 100;
    }

    public void UpdateGribBar(float gribValue)
    {
        //call event
        _updateGripBarUI.OnRiase(gribValue);
    }
}
