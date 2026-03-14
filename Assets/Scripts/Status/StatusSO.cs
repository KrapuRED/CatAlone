using UnityEngine;

[System.Serializable]
public enum StatusType
{
    None, 
    Hunger,
    Social,
    Happiness
}

[CreateAssetMenu(fileName = "StatusSO", menuName = "Game Data/StatusSO")]
public class StatusSO : ScriptableObject
{
    public string statusName;
    public StatusType statusType;
    [Range(0, 100)] public float deafultStatusPoint;
    [Range(0, 100)] public float saveStatusPoint;
}
