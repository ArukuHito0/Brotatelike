using ObjectPoolSystem;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class EnemyGenerator : MonoBehaviour
{
    public static EnemyGenerator Instance {  get; private set; }

    private ObjectPool enemyPool;

    [SerializeField] private List<WaveData> waveDataList;

    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private Transform fieldSize;
    [SerializeField] private float margin;

    [SerializeField] private TextMeshProUGUI waveCntText;

    private float spawnRangeX;
    private float spawnRangeY;

    public uint currentWaveCnt { get; private set; } = 0;
    public int waveIdx => (int)Mathf.Clamp(currentWaveCnt - 1, 0, waveDataList.Count);

    public event Action<int> OnUpdateWaveTime;
    [SerializeField] private UnityEvent OnStartWave;
    [SerializeField] private UnityEvent OnEndWave;

    private Coroutine activeWave;

    private void OnDestroy()
    {
        Instance = null;
    }

    private void Awake()
    {
        Instance = this;

        enemyPool = GameObject.Find("EnemyPool").GetComponent<ObjectPool>();

        spawnRangeX = fieldSize.localScale.x * 0.5f - margin;
        spawnRangeY = fieldSize.localScale.y * 0.5f - margin;
    }

    private void Start()
    {
        StartWave();
    }

    public void StartWave()
    {
        currentWaveCnt = (uint)Mathf.Clamp(currentWaveCnt + 1, 1, waveDataList.Count);

        if (activeWave != null)
        {
            StopCoroutine(activeWave);
            activeWave = null;
        }

        activeWave = StartCoroutine(SpawnEnemy(waveDataList[waveIdx]));

        if(waveCntText != null) waveCntText.text = $"Wave {currentWaveCnt}";

        OnStartWave?.Invoke();
    }

    private IEnumerator SpawnEnemy(WaveData wave)
    {
        foreach (WaveData.SpawnData spawnData in wave.spawns)
        {
            spawnData.SetSpawnTime(0);
        }

        float time = wave.waveTime;

        while (time >= 0)
        {
            foreach (WaveData.SpawnData spawnData in wave.spawns)
            {
                if (Time.time - spawnData.spawnTime >= spawnData.spawnInterbal)
                {
                    spawnData.SetSpawnTime(Time.time);

                    for (int i = 0; i < spawnData.spawnCnt; i++)
                        EnemyGenerate(spawnData.enemy);
                }
            }

            time -= Time.deltaTime;
            OnUpdateWaveTime?.Invoke((int)time);

            yield return null;
        }

        OnEndWave?.Invoke();
        ReleaseAllEnemy();
    }

    private void EnemyGenerate(EnemyStatusData enemyStatusData)
    {
        Vector3 spawnPos = new Vector3(UnityEngine.Random.Range(-spawnRangeX, spawnRangeX), UnityEngine.Random.Range(-spawnRangeY, spawnRangeY));

        EnemyBase enemy = enemyPool.GetPooledObject(spawnPos).GetComponent<EnemyBase>();
        enemy.Initialize(enemyStatusData);
    }

    private void ReleaseAllEnemy()
    {
        var list = EnemyBase.enemyList.ToList();

        foreach (EnemyBase enemy in list)
        {
            enemy.Release();
        }

        EnemyBase.enemyList.Clear();
    }
}
