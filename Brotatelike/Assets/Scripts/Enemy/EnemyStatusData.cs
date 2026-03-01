using UnityEngine;

[CreateAssetMenu(fileName = "EnemyStatusData", menuName = "EnemyStatusData")]
public class EnemyStatusData : ScriptableObject
{
    [SerializeField] private Sprite enemySprite;
    [SerializeField] private float maxHealth;
    [SerializeField] private float strength;
    [SerializeField] private float attackSpeed;
    [SerializeField] private float attackRange;
    [SerializeField] private float moveSpeed;

    public Sprite EnemySprite => enemySprite;
    public float MaxHealth => maxHealth;
    public float Strength => strength;
    public float AttackSpeed => attackSpeed;
    public float AttackRange => attackRange;
    public float MoveSpeed => moveSpeed;
}
