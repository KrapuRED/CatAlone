using System.Collections;
using TMPro;
using UnityEngine;

public class TextAreaNeighbourCatUI : MonoBehaviour
{
    public TextMeshProUGUI meowText;
    [SerializeField] private CanvasGroup _cg;

    private void Start()
    {
        _cg = GetComponent<CanvasGroup>();
    }

    public void SetMeowText(string meowWord)
    {
        meowText.text = meowWord;
        _cg.alpha = 1;
        StartCoroutine(VisibleMeowText());
    }

    private void HideMeowText()
    {
        _cg.alpha = 0;
        meowText.text = "";
    }

    IEnumerator VisibleMeowText()
    {
        yield return new WaitForSeconds(0.75f);
        HideMeowText();
    }
}
