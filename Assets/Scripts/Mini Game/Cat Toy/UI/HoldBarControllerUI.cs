using UnityEngine;
using UnityEngine.UI;

public class HoldBarControllerUI : MonoBehaviour
{
    [Header("Game Object")]
    [SerializeField] private GameObject _leftBar;
    [SerializeField] private GameObject _rightBar;
    [SerializeField] private GameObject _progressBar;
    [SerializeField] private float _currentProgress;

    [Header("UI")]
    [SerializeField] private float _middlePoint;
    [SerializeField] private Slider _sliderleftBar;
    [SerializeField] private Slider _sliderRightBar;
    [SerializeField] private Slider _sliderProgress;

    public void ShowBar(bool isRight)
    {
        if (isRight)
            _leftBar.SetActive(true);
        else
            _rightBar.SetActive(true);

        _progressBar.SetActive(true);
    }

    public void HideBar()
    {
        _rightBar.SetActive(false);
        _leftBar.SetActive(false);
        _progressBar.SetActive(false);
    }

    public void UpdaetProgressBar(float progress)
    {
        _sliderProgress.value = progress;
    }
}
