using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GripBarUI : MonoBehaviour
{
    [SerializeField] private GameObject _gribBar;
    [SerializeField] private Slider _gribBarStatic;
    [SerializeField] private Slider _gripBarDyinamic;
    [SerializeField] private UpdateGripBarUIEventSO _updateGripBarUI;
    [SerializeField] private TextMeshProUGUI _wordText;

    [Header("Events")]
    [SerializeField] private UpdateGripTextEventSO _updateGripText;

    public void SetGribBarStatic(float grib)
    {
        //Debug.Log("[GripBarUI - SetGribBarStatic] GribBar Static : " + grib);
        _gribBar.SetActive(true);
        _gribBarStatic.value = grib;
    }

    private void UpdateWord(string letter)
    {
        _wordText.text = letter;
    }

    private void CloseGripBar()
    {
        _gribBar.SetActive(false);
    }

    public void UpdateGribBar(float grib)
    {
        _gripBarDyinamic.value = grib / 100;
    }

    private void OnEnable()
    {
        _updateGripBarUI.Register(UpdateGribBar);
        _updateGripText.Register(UpdateWord);
    }

    private void OnDisable()
    {
        _updateGripBarUI.Unegister(UpdateGribBar);
        _updateGripText.Unregister(UpdateWord);

    }
}
