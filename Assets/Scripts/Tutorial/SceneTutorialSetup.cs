using System.Collections.Generic;
using UnityEngine;

public class SceneTutorialSetup : MonoBehaviour
{
    [SerializeField] private List<TutorialData> sceneTutorials;

    void Start()
    {
        TutorialManager.instance.RegisterSceneTutorials(sceneTutorials);
    }
}
