using UnityEngine;

public class EnemyRuntimeStatus : CharacterRuntimeStatusBase
{
    [SerializeField] private EnemyStatusData enemyStatusData;
    public EnemyStatusData EnemyStatusData => enemyStatusData;

    public void SetStatusData(EnemyStatusData enemyStatusData)
    {
        this.enemyStatusData = enemyStatusData;
    }

    public override float MaxHealth => enemyStatusData.BaseMaxHealth;
    public override float Strength => enemyStatusData.BaseStrength;
    public override float AttackSpeed => enemyStatusData.BaseAttackSpeed;
    public override float AttackRange => enemyStatusData.BaseAttackRange;
    public override float MoveSpeed => enemyStatusData.BaseMoveSpeed;
}
