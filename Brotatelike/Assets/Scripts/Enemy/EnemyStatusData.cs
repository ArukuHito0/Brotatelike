using UnityEngine;

[CreateAssetMenu(fileName = "EnemyStatusData", menuName = "EnemyStatusData")]
public class EnemyStatusData : ScriptableObject
{
    [SerializeField] private float baseMaxHealth;
    [SerializeField] private float baseStrength;
    [SerializeField] private float baseAttackSpeed;
    [SerializeField] private float baseAttackRange;
    [SerializeField] private float baseMoveSpeed;

    public float BaseMaxHealth => baseMaxHealth;
    public float BaseStrength => baseStrength;
    public float BaseAttackSpeed => baseAttackSpeed;
    public float BaseAttackRange => baseAttackRange;
    public float BaseMoveSpeed => baseMoveSpeed;
}
