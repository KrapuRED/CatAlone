using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [SerializeField] private bool _isMiniGameActive;
    [SerializeField] private bool _isTutorialDone;

    [SerializeField] private UpdateTutorialEventSO updateTutorial;

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

    public void SetITutorialActive()
    {
        Debug.Log("[GameManager - SetITutorialActive]");
        _isTutorialDone = true;
    }

    private void OnEnable()
    {
        updateTutorial.Register(SetITutorialActive);
    }
    private void OnDisable()
    {
        updateTutorial.Unregister(SetITutorialActive);
    }
}
