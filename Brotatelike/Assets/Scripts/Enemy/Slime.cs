using System.Collections;
using UnityEngine;

public class Slime : EnemyBase
{
    private float beforeAttacktime = 0;

    protected override void Attack()
    {
        if (Time.time - beforeAttacktime > enemyStatus.AttackSpeed)
        {
            PlayerController.Instance.HealthComponent.TakeDamage(enemyStatus.Strength);
            beforeAttacktime = Time.time;
        }

    }

    protected override void Move()
    {
        transform.position += (PlayerController.Instance.transform.position - transform.position).normalized * enemyStatus.MoveSpeed * Time.deltaTime;
    }
}
