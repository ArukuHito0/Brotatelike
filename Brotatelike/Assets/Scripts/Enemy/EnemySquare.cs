using System.Collections;
using UnityEngine;

public class EnemySquare : EnemyBase
{
    public override IEnumerator AttackCoroutine(HealthComponent target)
    {
        float time = status.AttackSpeed;

        while (target)
        {
            yield return new WaitUntil(() => GetTarget.GetPlayerInRange(transform.position, status.AttackRange));

            time += Time.deltaTime;

            if(time >= status.AttackSpeed)
            {
                target?.TakeDamage(status.Strength);
                time = 0;
            }

            if (target == null || target.gameObject.activeSelf == false)
            {
                yield break;
            }

            if (!GetTarget.GetPlayerInRange(transform.position, status.AttackRange))
            {
                time = status.AttackSpeed;
            }

        }
    }
}
