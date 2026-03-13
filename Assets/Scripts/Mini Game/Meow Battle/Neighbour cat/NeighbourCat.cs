using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NeighbourCat : MonoBehaviour
{
    [SerializeField] private float _timerMeow;
    [SerializeField] private bool _isWaiting;
    [SerializeField] private List<SpawnRate> _spawnRateList = new List<SpawnRate>();
    [SerializeField] private CatAniamtion _catAniamtion;

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

        _timerMeow = GetDelayMeow();

        _isWaiting = true;
        _meowRoutine = StartCoroutine(WaitingToMeow());
    }

    private float GetDelayMeow()
    {
        int randomValue = Random.Range(0, 100);

        int cumulative = 0;

        foreach (SpawnRate rate in _spawnRateList)
        {
            cumulative += rate.rate;

            if (randomValue < cumulative)
            {
                return rate.spawnRate;
            }
        }
        return 0;
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
        _catAniamtion.PlayTriggerAnimation("meow");
        AudioManager.instance.PlaySoundEffect("Meow");

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
