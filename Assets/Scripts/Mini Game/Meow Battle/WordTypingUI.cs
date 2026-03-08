using TMPro;
using UnityEngine;

public class WordTypingUI : MonoBehaviour
{
    public TextMeshProUGUI wordTyping;

    public void SetWordtyping(string word)
    {
        wordTyping.text = word;
    }
}
