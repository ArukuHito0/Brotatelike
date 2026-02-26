using UnityEngine;

public class EnemyRuntimeStatus : CharacterRuntimeStatusBase
{
    [SerializeField] private EnemyStatusData enemyStatusData;

    public void SetStatusData(EnemyStatusData enemyStatusData)
    {
        this.enemyStatusData = enemyStatusData;
    }

    public override float MaxHealth => enemyStatusData.BaseMaxHealth + bonusMaxHealth;
    public override float Strength => enemyStatusData.BaseStrength + bonusStrength;
    public override float AttackSpeed => enemyStatusData.BaseAttackSpeed + bonusAttackSpeed;
    public override float AttackRange => enemyStatusData.BaseAttackRange * (1 + bonusAttackRange / 100);
    public override float MoveSpeed => enemyStatusData.BaseMoveSpeed * (1 + bonusMoveSpeed / 100);

    public int bonusMaxHealth { get; set; }
    public int bonusStrength { get; set; }
    public int bonusAttackSpeed { get; set; }
    public int bonusAttackRange { get; set; }
    public int bonusMoveSpeed { get; set; }
}
