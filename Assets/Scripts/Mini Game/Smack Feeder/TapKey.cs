using TMPro;
using UnityEngine;
using System.Collections.Generic;

[System.Serializable]
public class TimingPhase
{
    public string timingName;
    public float timingTime;
}

public class TapKey : MonoBehaviour
{
    [Header("Tap Key Config")]
    [SerializeField] private TextMeshPro tapKeyText;
    [SerializeField] private string tapKey;
    
    [Header("Timing Config")]
    [SerializeField] private float _targetTiming;
    [SerializeField] private List<TimingPhase> timingPhaseList = new List<TimingPhase>();
    [SerializeField] private float _currentTiming;
    [SerializeField] private bool _canTap;

    public TapKeyAnimation tapKeyAnimation;
    [Header("Events")]
    public ReadyToTapEventSO readeyTapEvent;
    public RemoveTapKeyEventSO removeTapEvent;

    private void Start()
    {
        _currentTiming = _targetTiming;
    }

    public void SetTapKet(string keyWord)
    {
        tapKey = keyWord;
        tapKeyText.text = tapKey;
        _currentTiming = _targetTiming;

        tapKeyAnimation.StartApproachAnimation(_targetTiming);

        //tell SmackFeederManager : Hey im waiting get clicked!
        readeyTapEvent.OnReadyToTap(this);
    }

    private void Update()
    {
        _currentTiming -= Time.deltaTime;

        if (_currentTiming <= 0)
        {
            removeTapEvent.OnRaise(this);
            Destroy(gameObject);
        }
    }

    public bool IsCorrectKey(string key)
    {
        if (tapKey != key)
            return false;

        return true;
    }

    public string CheckTiming(float playerTiming)
    {
        float diff = Mathf.Abs(playerTiming - _targetTiming); ;
        for (int i = 0; i < timingPhaseList.Count; i++)
        {
            if ( diff <= timingPhaseList[i].timingTime)
            {
                return timingPhaseList[i].timingName;
            }
        }
        return "MISS";
    }

    public void OnCorrectKey()
    {
        Destroy(gameObject);
    }
}
