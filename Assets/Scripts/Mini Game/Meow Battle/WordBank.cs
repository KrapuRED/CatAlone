using UnityEngine;
using System.Linq;
using System.Collections.Generic;

[System.Serializable]
public class OriginalWord
{
    public string language;
    public string catSound;
}

public class WordBank : MonoBehaviour
{
    public List<OriginalWord> originalWords = new List<OriginalWord>();

    [SerializeField] private List<OriginalWord> workingWords = new List<OriginalWord>();

    private void Awake()
    {
        RefillWords();
    }

    private void RefillWords()
    {
        workingWords.AddRange(originalWords);
        ShuffleWord(workingWords);
        ConvertToLower(workingWords);
    }

    private void ShuffleWord(List<OriginalWord> listCatSound)
    {
        for (int i = 0; i < listCatSound.Count; i++)
        {
            int random = Random.Range(i, listCatSound.Count);

            OriginalWord temp = listCatSound[i];
            listCatSound[i] = listCatSound[random];
            listCatSound[random] = temp;
        }
    }

    private void ConvertToLower(List<OriginalWord> listCatSound)
    {
        for (int i = 0;i < listCatSound.Count;i++)
        {
            listCatSound[i].catSound = listCatSound[i].catSound.ToLower();
        }
    }

    public string GetWord()
    {
        if (workingWords.Count == 0)
        {
            RefillWords();
        }

        OriginalWord newWord = workingWords[workingWords.Count - 1];
        workingWords.RemoveAt(workingWords.Count - 1);

        return newWord.catSound;
    }
}
