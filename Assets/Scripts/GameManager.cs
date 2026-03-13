using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [SerializeField] private TimerUI timerUI;
    [SerializeField] private bool _isMiniGameActive;
    [SerializeField] private bool _isTutorialDone;
    [SerializeField] private float _durationGame;
    [SerializeField] private float _currentGameDuration;

    [SerializeField] private UpdateTutorialEventSO updateTutorial;
    [SerializeField] private UpdateTimerUIEventSO updateTimerUI;

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

    private void Update()
    {
        if (!_isMiniGameActive && _isTutorialDone && _currentGameDuration > 0)
        {
            _currentGameDuration -= Time.deltaTime;
            updateTimerUI.Raise(_currentGameDuration);

            if (_currentGameDuration <= 0)
            {
                ManagerPanel.instance.OpenPanel("EndGamePanel");
            }
        }
    }

    public void SetIsMiniGameActive(bool isActive)
    {
        _isMiniGameActive = isActive;
    }

    public void SetITutorialActive()
    {
        Debug.Log("[GameManager - SetITutorialActive]");
        _currentGameDuration = _durationGame;
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
