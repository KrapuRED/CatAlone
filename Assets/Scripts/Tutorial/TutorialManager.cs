using UnityEngine;
using System.Collections.Generic;
using System.Linq;

[System.Serializable]
public class TutorialData
{
    public string tutorialName;
    public Tutorial tutorialData;
    public GameObject tutprialGO;
}

public class TutorialManager : MonoBehaviour
{
    public static TutorialManager instance;

    [SerializeField] private List<TutorialData> tutorials = new List<TutorialData>();
    [SerializeField] private HashSet<string> completedTutorials = new HashSet<string>();
    [SerializeField] private Tutorial _activeTutorial;

    [SerializeField] private UpdateTutorialEventSO updateTutorial;
    
    public int activeTutorial => tutorials.Count;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void CheckEnterLetter(string typingLetter)
    {
        if (GameManager.instance.isTutorialDone)
        {
            return;
        }

        if (_activeTutorial == null && tutorials.Count > 0)
            _activeTutorial = tutorials.First().tutorialData;

        _activeTutorial.CheckLetter(typingLetter);
    }

    public void RegisterSceneTutorials(List<TutorialData> sceneTutorials)
    {
        tutorials.Clear();

        foreach (var tutorial in sceneTutorials)
        {
            if (!IsTutorialCompleted(tutorial.tutorialName))
            {
                tutorials.Add(tutorial);
            }
        }

        if (tutorials.Count > 0)
        {
            _activeTutorial = tutorials.First().tutorialData;
            _activeTutorial.gameObject.SetActive(true);
        }
    }

    private bool IsTutorialCompleted(string tutorialName)
    {
        return completedTutorials.Contains(tutorialName); ;
    }

    public void CompleteTutorial(string tutorialName)
    {
        completedTutorials.Add(tutorialName);
    }

    public void NextTutorial(Tutorial activeTutorial)
    {
        CompleteTutorial(tutorials.First().tutorialName);
        RemoveTutorialData(activeTutorial);

        if (tutorials.Count <= 0)
        {
            //Event
            Debug.Log("[TutorialManager - NextTutorial] All tutorial is Done!");
            updateTutorial.OnRaise();
            _activeTutorial = null;
            return;
        }

        _activeTutorial = tutorials.First().tutorialData;
        _activeTutorial.gameObject.SetActive(true);
    }

    private void RemoveTutorialData(Tutorial activeTutorial)
    {
        tutorials.RemoveAll(t => t.tutorialData == activeTutorial);
    }
}
