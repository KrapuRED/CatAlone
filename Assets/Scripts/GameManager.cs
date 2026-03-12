using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [SerializeField] private bool _isMiniGameActive;
    [SerializeField] private bool _isTutorialDone;
    public bool isMiniGameActive => _isMiniGameActive;
    public bool isTutorialDone => _isTutorialDone;

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

    public void SetIsMiniGameActive(bool isActive)
    {
        _isMiniGameActive = isActive;
    }

    public void SetITutorialActive(bool isActive)
    {
        _isMiniGameActive = isActive;
    }
}
