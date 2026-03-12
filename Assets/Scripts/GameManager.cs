using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [SerializeField] private bool _isMiniGameActive;
    public bool isMiniGameActive => _isMiniGameActive;

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
}
