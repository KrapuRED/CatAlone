using UnityEngine;
using System.Collections.Generic;

public class TapKeyWord : MonoBehaviour
{
    [Header("Key Tap Config")]
    [SerializeField] private List<KeyCode> keyWords = new List<KeyCode>();

    public string GetKeyWord()
    {
        int randomKey = Random.Range(0, keyWords.Count);

        return keyWords[randomKey].ToString();
    }
}
