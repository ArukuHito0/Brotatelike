using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "EnemyStatusData", menuName = "EnemyStatusData")]
public class EnemyStatusData : ScriptableObject
{
    // ドロップの情報
    [System.Serializable]
    public class DropItemConfig
    {
        public PooledObject itemPrefab;   // アイテムのプレファブ
        public int baseDropCnt;         // ドロップ数
        public int baseDropChance;      // ドロップ率
    }

    [Header("基礎ステータス")]
    [SerializeField] private float maxHealth;
    [SerializeField] private float strength;
    [SerializeField] private float attackSpeed;
    [SerializeField] private float attackRange;
    [SerializeField] private float moveSpeed;

    [Header("ウェーブ毎の増加量")]
    [SerializeField] private float wavePerHealth;
    [SerializeField] private float wavePerStrength;

    [Header("出現し始めるウェーブ数")]
    [SerializeField] private int spawnMinWave;

    [Header("ドロップするアイテム")]
    public List<DropItemConfig> dropItemList;

    public float MaxHealth => maxHealth + ((EnemyGenerator.Instance.currentWaveCnt - spawnMinWave) * wavePerHealth);
    public float Strength => strength + ((EnemyGenerator.Instance.currentWaveCnt - spawnMinWave) * wavePerStrength);
    public float AttackSpeed => attackSpeed;
    public float AttackRange => attackRange;
    public float MoveSpeed => moveSpeed;
}
