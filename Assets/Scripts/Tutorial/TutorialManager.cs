using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class TutorialManager : MiniGame
{
    public static TutorialManager instance;

    [SerializeField] private List<Tutorial> tutorials = new List<Tutorial>();
    [SerializeField] private Tutorial _activeTutorial;

    private void Awake()
    {
        if (instance == null) 
            instance = this;
        else
            Destroy(gameObject);
    }

    public override void CheckEnterLetter(string typingLetter)
    {
        if (_activeTutorial != null && !GameManager.instance.isTutorialDone)
        {
            _activeTutorial.CheckLetter(typingLetter);
            return;
        }

        _activeTutorial = tutorials.First();
        _activeTutorial.CheckLetter(typingLetter);
    }

    public void NextTutorial(Tutorial activeTutorial)
    {
        tutorials.Remove(activeTutorial);

        if (tutorials.Count <= 0)
        {
            //Event
            Debug.Log("[TutorialManager - NextTutorial] All tutorial is Done!");
            _activeTutorial = null;
            return;
        }

        _activeTutorial = tutorials.First();
    }
}
