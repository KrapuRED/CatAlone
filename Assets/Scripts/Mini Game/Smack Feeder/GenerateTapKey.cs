using UnityEngine;
using System.Collections.Generic;

public class GenerateTapKey : MonoBehaviour
{
    [Header("Generate TapKey Config")]
    [SerializeField] private GameObject tapKeyPrefab;
    [SerializeField] private Transform _spawnAreaTapKeyContainer;
    [SerializeField] private List<Transform> _spawnAreaTapKey = new List<Transform>();

    private void Start()
    {
        if (_spawnAreaTapKeyContainer != null)
        {
            AutoAssignSpawnPoint();
        }
    }

    private void AutoAssignSpawnPoint()
    {
        Transform[] spawnPoints = _spawnAreaTapKeyContainer.GetComponentsInChildren<Transform>();

        foreach (Transform spawnPoint in spawnPoints)
        {
            if (spawnPoint.CompareTag("Spawner"))
                _spawnAreaTapKey.Add(spawnPoint);
        }
    }

    public void OnGenerateTapKey(string keyword)
    {
        //Debug.Log("[GenerateTapKey] Generate tap with : " + keyword);
        //Debug.Log("[GenerateTapKey] Generate tap spawn in " + GetSpawnLocation().transform);

        Transform spawnLoc = GetSpawnLocation();

        if (spawnLoc == null)
            return;

        GameObject newKeyTap = Instantiate(tapKeyPrefab, spawnLoc);

        if (newKeyTap == null)
        {
            Debug.LogError("Succes to Instantiate but still NULL!");
            return;
        }

        newKeyTap.name = $"TapKey - {keyword}";
        TapKey newKey = newKeyTap.GetComponent<TapKey>();
        newKey.SetTapKet(keyword);
    }

    private Transform GetSpawnLocation()
    {
        int random = Random.Range(0, _spawnAreaTapKey.Count);

        if (_spawnAreaTapKey[random].childCount == 0)
            return _spawnAreaTapKey[random];

        //Debug.Log($"{_spawnAreaTapKey[random]} is been fill!");
        return null;
    }
}
