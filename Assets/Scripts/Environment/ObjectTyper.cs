using UnityEngine;
using TMPro;

public class ObjectTyper : MonoBehaviour
{
    //SO for object word data
    [SerializeField] private ObjectTyperSO ObjectTyperData;
    public string word => ObjectTyperData.wordLetter;
    [SerializeField] private TextMeshProUGUI wordTextUI;
    [SerializeField] private string soundName;
    [SerializeField] private bool isCompleted;

    private void Start()
    {
        SetWordText(ObjectTyperData.wordLetter);
    }

    private void SetWordText(string wordLetter)
    {
        wordTextUI.text = wordLetter;
    }



    public void OnWordCompleted()
    {
        //Debug.Log($"{ObjectTyperData.wordLetter} is complete!");
        isCompleted = true;

        AudioManager.instance.PlaySoundEffect(soundName);
        //LevelManager to change the scene and start to play the minigame
        if (ObjectTyperData.isCanChangeScene)
            LevelManager.instance.ChangeScene(ObjectTyperData.nameObject);
    }
}
