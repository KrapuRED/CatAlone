using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public static LevelManager instance;
    [SerializeField] private string currentScene;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
            Destroy(gameObject);
    }

    public void ChangeScene(string nameScene){
        Debug.Log("rate to " + nameScene);
        currentScene = nameScene;
        StatusManager.instance.RefreshUI();
        SceneManager.LoadScene(nameScene);
    }

    public bool CheckHomeScene()
    {
        if (currentScene == "HomeScene")
            return true;

        return false;
    }
}
