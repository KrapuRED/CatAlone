using UnityEngine;

public class MiniGameManager : MonoBehaviour
{
    public static MiniGameManager instance;

    [SerializeField] private MiniGame miniGameActive;

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
        if (miniGameActive != null)
            miniGameActive.CheckEnterLetter(typeLetter);

    }
}
