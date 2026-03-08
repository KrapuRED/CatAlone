using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public static LevelManager instance;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }

    public void ChangeScene(string nameScene){
        Debug.Log("Change to " + nameScene);
        SceneManager.LoadScene(nameScene);
    }
}
