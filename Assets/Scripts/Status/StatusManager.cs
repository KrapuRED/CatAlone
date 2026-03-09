using UnityEngine;

public class StatusManager : MonoBehaviour
{
    public static StatusManager instance;

    [SerializeField] private float _statusHunger;
    [SerializeField] private float _statusSocial;
    [SerializeField] private float _statusFun;


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

    public void ChangeStatusPoint(MiniGameType type, float status)
    {
        switch (type)
        {
            case MiniGameType.BattleMeow:
                _statusSocial += status;
                break;

            case MiniGameType.Feeding:
                _statusHunger += status;
                break;
            case MiniGameType.CatToy:
                _statusHunger += status;
                break;
        }
    }
}
