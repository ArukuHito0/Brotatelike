using UnityEngine;

public static class GetTarget
{
    public static EnemyBase GetTargetInRange(Vector3 ownerPos, float range)
    {
        EnemyBase _enemy = null;
        float minDistance = range * range;

        if (EnemyBase.enemyList.Count <= 0) return null;

        foreach (EnemyBase enemy in EnemyBase.enemyList)
        {
            float sqrDist = (ownerPos - enemy.transform.position).sqrMagnitude;

            if (sqrDist <= minDistance)
            {
                minDistance = sqrDist;
                _enemy = enemy;
            }
        }

        return _enemy;
    }
}
