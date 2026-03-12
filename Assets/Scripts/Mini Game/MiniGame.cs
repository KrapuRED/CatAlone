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

[System.Serializable]
public class MiniGameDuration
{
    public float Duration;
    public int change;
}

[System.Serializable]
public class SpawnRate
{
    public float spawnRate;
    public int rate;
}

public class MiniGame : MonoBehaviour
{
    public MiniGameType type;

    public virtual void CheckEnterLetter(string typingLetter)
    {

    }
}
