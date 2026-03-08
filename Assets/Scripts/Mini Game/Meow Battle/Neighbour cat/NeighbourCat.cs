using System.Collections;
using UnityEngine;

public class NeighbourCat : MonoBehaviour
{
    [SerializeField] private float _timerMeow;

    [Header("Events")]
    [SerializeField] private StartBattleEventSO _startBattleEvent;
    [SerializeField] private EndBattleEventSO _endBattleEvent;

    private Coroutine _meowRoutine;
    private void StartToWaiting()
    {
        if (_meowRoutine == null)
        {
            _meowRoutine = StartCoroutine(WaitingToMeow());
        }
    }

    private IEnumerator WaitingToMeow()
    {
        while (true)
        {
            Debug.Log("Neighbour Cat is waiting to meowing");
            yield return new WaitForSeconds(_timerMeow);
            Debug.Log("Neighbour Cat win the meow! next :D");
            MeowBattleManager.instance.NeighbourCatMeowing();
            yield return new WaitForSeconds(1);
        }
    }

    private void StopWaiting()
    {
        if (_meowRoutine != null)
        {
            StopCoroutine(_meowRoutine);
            _meowRoutine = null;
        }
    }

    private void OnEnable()
    {
        _startBattleEvent.Register(StartToWaiting);
        _endBattleEvent.Register(StopWaiting);
    }

    private void OnDisable()
    {
        _startBattleEvent.Unregister(StartToWaiting);
        _endBattleEvent.Unregister(StopWaiting);
        StopWaiting();
    }
}
