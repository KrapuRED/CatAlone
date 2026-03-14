using System.Transactions;
using UnityEngine;
using UnityEngine.Playables;

public class CheckDurationCutScene : MonoBehaviour
{
    [SerializeField] private PlayableDirector _director;
    public string nextScene;
    public string panelName;

    private void OnEnable()
    {
        if (_director != null)
            _director.stopped += CutSceneEnd;
    }

    private void OnDisable()
    {
        if (_director != null)
            _director.stopped -= CutSceneEnd;
    }

    private void CutSceneEnd(PlayableDirector director)
    {
        if (!string.IsNullOrEmpty(nextScene))
        {
            LevelManager.instance.ChangeScene(nextScene);
            return;
        }

        ManagerPanel.instance.OpenPanel(panelName);
    }
}
