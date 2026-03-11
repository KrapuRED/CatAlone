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

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
            Destroy(gameObject);
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
            StatusManager.instance.ChangeStatusPoint(miniGameType, _gainWin);
        else
            StatusManager.instance.ChangeStatusPoint(miniGameType, _gainLoose);

        ManagerPanel.instance.OpenPanel("EndMiniGame");
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
