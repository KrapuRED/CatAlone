using UnityEngine;

[System.Serializable]
public enum MiniGameType
{
    None,
    Feeding,
    BattleMeow,
    CatToy,
    Sleep
}

public class MiniGame : MonoBehaviour
{
    public MiniGameType type;

    public virtual void CheckEnterLetter(string typingLetter)
    {

    }
}
