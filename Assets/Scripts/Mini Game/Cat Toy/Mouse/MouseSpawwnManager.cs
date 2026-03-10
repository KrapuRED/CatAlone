using UnityEngine;

public class MouseSpawwnManager : MonoBehaviour
{
    public static MouseSpawwnManager instance;

    [Header("Mouse Spawner Config")]
    [SerializeField] private GameObject mousePrefab;
    [SerializeField] private Transform spawnContinaer;
    [SerializeField] private float _spawnTime;

    private void Awake()
    {
        if (instance == null) 
            instance = this;
        else
            Destroy(gameObject);
    }
}
