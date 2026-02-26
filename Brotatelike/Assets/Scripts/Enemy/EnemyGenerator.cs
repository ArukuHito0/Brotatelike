using ObjectPoolSystem;
using System.Collections;
using UnityEngine;

public class EnemyGenerator : MonoBehaviour
{
    private ObjectPool enemyPool;

    private PlayerController player;

    [SerializeField] private EnemyStatusData enemyStatusData;

    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private Transform fieldSize;
    [SerializeField, Range(1f, 30f)]
    private float spawnInterbal;
    [SerializeField] private float margin;

    private float spawnRangeX;
    private float spawnRangeY;

    private void Awake()
    {
        player = GameObject.Find("Player").GetComponent<PlayerController>();
        enemyPool = GameObject.Find("EnemyPool").GetComponent<ObjectPool>();

        spawnRangeX = fieldSize.localScale.x * 0.5f - margin;
        spawnRangeY = fieldSize.localScale.y * 0.5f - margin;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        StartCoroutine(EnemyGenerate());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private IEnumerator EnemyGenerate()
    {
        while (!player.IsDead)
        {
            Vector3 spawnPos = Vector3.zero;

            spawnPos.x = Random.Range(-spawnRangeX, spawnRangeX);
            spawnPos.y = Random.Range(-spawnRangeY, spawnRangeY);

            EnemyBase enemy = enemyPool.GetPooledObject().GetComponent<EnemyBase>();
            enemy.GetComponent<EnemyRuntimeStatus>().SetStatusData(enemyStatusData);
            enemy.GetComponent<HealthComponent>().SetHealth();
            enemy.transform.position = spawnPos;

            yield return new WaitForSeconds(spawnInterbal);
        }
    }
}
