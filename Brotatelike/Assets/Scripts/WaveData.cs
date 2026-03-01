using System;
using System.Collections;
using UnityEngine;
using ObjectPoolSystem;

[CreateAssetMenu(fileName = "NewWaveData", menuName = "WaveData")]
public class WaveData : ScriptableObject
{
    [Serializable]
    public class SpawnData
    {
        public EnemyStatusData enemy;
        public int spawnCnt;
        public float spawnInterbal;

        public float spawnTime { get; private set; } = 0;

        public void SetSpawnTime(float spawnTime) => this.spawnTime = spawnTime;
    }

    public int waveTime;
    public SpawnData[] spawns;
}