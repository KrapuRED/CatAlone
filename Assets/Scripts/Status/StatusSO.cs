using UnityEngine;

[CreateAssetMenu(fileName = "StatusSO", menuName = "Game Data/StatusSO")]
public class StatusSO : ScriptableObject
{
    public string statusName;
    [Range(0, 100)] public float deafultStatusPoint;
    [Range(0, 100)] public float saveStatusPoint;
}
