using System.Collections;
using UnityEngine;

public class EnemySquare : EnemyBase
{
    public override IEnumerator AttackCoroutine(HealthComponent target)
    {
        float time = enemyStatus.AttackSpeed;

        while (target)
        {
            yield return new WaitUntil(() => GetTarget.GetPlayerInRange(transform.position, enemyStatus.AttackRange));

            time += Time.deltaTime;

            if(time >= enemyStatus.AttackSpeed)
            {
                target?.TakeDamage(enemyStatus.Strength);
                time = 0;
            }

            if (target == null || target.gameObject.activeSelf == false)
            {
                yield break;
            }

            if (!GetTarget.GetPlayerInRange(transform.position, enemyStatus.AttackRange))
            {
                time = enemyStatus.AttackSpeed;
            }

        }
    }

    public override void Move()
    {
        transform.position += (PlayerController.Instance.transform.position - transform.position).normalized * enemyStatus.MoveSpeed * Time.deltaTime;
    }
}
