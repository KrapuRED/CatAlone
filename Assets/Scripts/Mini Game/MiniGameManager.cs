using UnityEngine;

public enum GameResult
{
    Win,
    Loose
}

public class MiniGameManager : MonoBehaviour
{
    public static MiniGameManager instance;

    [SerializeField] private MiniGame miniGameActive;

    [SerializeField] private float _gainWin;
    [SerializeField] private float _gainLoose;
    [SerializeField] private bool _isMiniGameEnd;
    [SerializeField] private CheckLetterEventSO checkLetterEvent;
    [SerializeField] private UpdateRewardEventSO updateRewardEvent;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
            Destroy(gameObject);
    }

    private void Start()
    {
        GameManager.instance.SetIsMiniGameActive(true);
    }
    public void CheckTyping(string typeLetter)
    {
        //Debug.Log("[MiniGameManager] Letter : " + typeLetter);
        if (miniGameActive != null && !_isMiniGameEnd)
            miniGameActive.CheckEnterLetter(typeLetter);
    }

    public void EndMiniGame(MiniGameType miniGameType, GameResult result)
    {
        if (_isMiniGameEnd)
            return;

        if (result == GameResult.Win)
        {
            updateRewardEvent.OnRiase(miniGameType, _gainWin);
            StatusManager.instance.ChangeStatusPoint(miniGameType, _gainWin);
        }
           
        else
        {
            updateRewardEvent.OnRiase(miniGameType, _gainLoose);
            StatusManager.instance.ChangeStatusPoint(miniGameType, _gainLoose);
        }


        GameManager.instance.SetIsMiniGameActive(false);
        
        if (miniGameType != MiniGameType.None)
        {
            AudioManager.instance.PlaySoundEffect("MiniGameEnd");
            ManagerPanel.instance.OpenPanel("EndMiniGame");
        }

        _isMiniGameEnd = true;
    }

    private void OnEnable()
    {
        checkLetterEvent.Register(CheckTyping);
    }

    private void OnDisable()
    {
        checkLetterEvent.Unregister(CheckTyping);
    }
}
