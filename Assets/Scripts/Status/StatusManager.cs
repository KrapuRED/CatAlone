using UnityEngine;
using System.Collections;

public class StatusManager : MonoBehaviour
{
    public static StatusManager instance;

    [SerializeField] private float _statusHunger = 50f;
    [SerializeField] private float _statusSocial = 50f;
    [SerializeField] private float _statusHappines = 50f;

    public float GetHunger() => _statusHunger;
    public float GetSocial() => _statusSocial;
    public float GetHappiness() => _statusHappines;

    [SerializeField] private UpdateStatusUIEventSO updateStatusUI;

    private const float MAX_STATUS = 100f;
    private const float MIN_STATUS = 0f;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        RefreshUI();

        StartCoroutine(HungerDeplete());
        StartCoroutine(SocialDeplete());
        StartCoroutine(HappinessDeplete());
    }

    // -------------------------
    // STAT DEPLETION
    // -------------------------

    IEnumerator HungerDeplete()
    {
        while (true)
        {
            yield return new WaitForSeconds(5f);

            if (_statusHunger > 0)
                _statusHunger -= 1f;

            RefreshUI();
        }
    }

    IEnumerator SocialDeplete()
    {
        while (true)
        {
            yield return new WaitForSeconds(30f);

            if (_statusSocial > 0)
                _statusSocial -= 10f;

            RefreshUI();
        }
    }

    IEnumerator HappinessDeplete()
    {
        while (true)
        {
            yield return new WaitForSeconds(20f);

            if (_statusHappines > 0)
                _statusHappines -= 5f;

            RefreshUI();
        }
    }

    // -------------------------
    // MINIGAME REWARD
    // -------------------------

    public void ChangeStatusPoint(MiniGameType type, float status)
    {
        switch (type)
        {
            case MiniGameType.BattleMeow:
                _statusSocial += status;
                break;

            case MiniGameType.Feeding:
                _statusHunger += status;
                break;

            case MiniGameType.CatToy:
                _statusHappines += status;
                break;
        }

        RefreshUI();
    }

    // -------------------------
    // UI UPDATE
    // -------------------------

    public void RefreshUI()
    {
        _statusHunger = Mathf.Clamp(_statusHunger, MIN_STATUS, MAX_STATUS);
        _statusSocial = Mathf.Clamp(_statusSocial, MIN_STATUS, MAX_STATUS);
        _statusHappines = Mathf.Clamp(_statusHappines, MIN_STATUS, MAX_STATUS);

        updateStatusUI.OnRaise(_statusHunger, _statusSocial, _statusHappines);

        Debug.Log($"[StatusManager] Hunger: {_statusHunger} | Social: {_statusSocial} | Happiness: {_statusHappines}");
    }
}