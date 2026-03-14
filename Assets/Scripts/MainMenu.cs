using UnityEngine;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private string HomeSceneMusic;

    private void Start()
    {
        MusicManager.instance.PlayMusicBackground(HomeSceneMusic);
    }

    public void StartGame()
    {
        LevelManager.instance.ChangeScene("Opening");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
