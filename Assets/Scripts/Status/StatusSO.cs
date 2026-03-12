using UnityEngine;

[System.Serializable]
public enum StatusType
{
    None, 
    Hungger,
    Social,
    Happines
}

[CreateAssetMenu(fileName = "StatusSO", menuName = "Game Data/StatusSO")]
public class StatusSO : ScriptableObject
{
    public string statusName;
    public StatusType statusType;
    [Range(0, 100)] public float deafultStatusPoint;
    [Range(0, 100)] public float saveStatusPoint;
}
