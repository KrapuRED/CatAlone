using UnityEngine;
using System.Collections;

public class StatusManager : MonoBehaviour
{
    public static StatusManager instance;

    [SerializeField] private float _statusHunger;
    [SerializeField] private float _statusSocial;
    [SerializeField] private float _statusHappines;

    public float GetHunger() => _statusHunger;
    public float GetSocial() => _statusSocial;
    public float GetHappiness() => _statusHappines;

    [SerializeField] private UpdateStatusUIEventSO updateStatusUI;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
            Destroy(gameObject);
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
            yield return new WaitForSeconds(1f);
            _statusHunger -= 1f;
            RefreshUI();
        }
    }

    IEnumerator SocialDeplete()
    {
        while (true)
        {
            yield return new WaitForSeconds(18f);
            _statusSocial -= 15f;
            RefreshUI();
        }
    }

    IEnumerator HappinessDeplete()
    {
        while (true)
        {
            yield return new WaitForSeconds(12f);
            _statusHappines -= 18f;
            RefreshUI();
        }
    }

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

    public void RefreshUI()
    {
        updateStatusUI.OnRaise(_statusHunger, _statusSocial, _statusHappines);
        Debug.Log($"[StatusManager] Hunger: {_statusHunger} Social: {_statusSocial} Happiness: {_statusHappines}");
    }
}