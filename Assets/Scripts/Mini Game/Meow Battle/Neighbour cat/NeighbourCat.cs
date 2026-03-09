using System.Collections;
using UnityEngine;

public class NeighbourCat : MonoBehaviour
{
    [SerializeField] private float _timerMeow;
    [SerializeField] private bool _isWaiting;

    [Header("Word Bank")]
    [SerializeField] private WordBank _wordBank;
    [SerializeField] private string _currentWord;

    [Header("UI")]
    [SerializeField] private TextAreaNeighbourCatUI textAreaNeighbourCatUI;

    [Header("Events")]
    [SerializeField] private StartBattleEventSO _startBattleEvent;
    [SerializeField] private EndBattleEventSO _endBattleEvent;

    [SerializeField] private Coroutine _meowRoutine;
    
    private void StartToWaiting()
    {
        if (_meowRoutine != null)
            return;

        _isWaiting = true;
        _meowRoutine = StartCoroutine(WaitingToMeow());
    }

    private IEnumerator WaitingToMeow()
    {
        while (_isWaiting)
        {
            yield return new WaitForSeconds(_timerMeow);

            if (!_isWaiting)
                break;

            DoMeow();

            yield return new WaitForSeconds(1);
        }

        _meowRoutine = null;
    }

    private void DoMeow()
    {
        _currentWord = _wordBank.GetWordNeighbour();

        //Debug.Log("Neighbour Cat : " + _currentWord);

        textAreaNeighbourCatUI.SetMeowText(_currentWord);
        MeowBattleManager.instance.NeighbourCatMeowing();
    }

    private void StopWaiting()
    {
        _isWaiting = false;

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
    }
}
