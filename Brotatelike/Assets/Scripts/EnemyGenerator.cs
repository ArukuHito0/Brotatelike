using System.Collections;
using UnityEngine;

public class EnemyGenerator : MonoBehaviour
{
    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private Transform fieldSize;
    [SerializeField, Range(1f, 30f)]
    private float spawnInterbal;
    [SerializeField] private float margin;

    private float spawnRangeX;
    private float spawnRangeY;

    private void Awake()
    {
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
        while (true)
        {
            Vector3 spawnPos = Vector3.zero;

            spawnPos.x = Random.Range(-spawnRangeX, spawnRangeX);
            spawnPos.y = Random.Range(-spawnRangeY, spawnRangeY);

            Instantiate(enemyPrefab, spawnPos, Quaternion.identity);

            yield return new WaitForSeconds(spawnInterbal);
        }
    }
}
