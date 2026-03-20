using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public abstract class EnemyBase : PooledObject
{
    public static List<EnemyBase> enemyList = new List<EnemyBase>();

    [SerializeField] private EnemyStatusData _enemyStatusData;
    public EnemyStatusData enemyStatus { get => _enemyStatusData; private set => _enemyStatusData = value; }

    private HealthComponent healthComponent;

    [SerializeField] private EmitEffect defeatedEffect;

    protected Rigidbody2D rb;

    private Coroutine attackCoroutine = null;

    protected override void OnSpawn()
    {
        enemyList.Add(this);
    }

    public void Initialize()
    {
        healthComponent.SetHealth(enemyStatus.MaxHealth);
    }

    private void OnDisable()
    {
        if (attackCoroutine != null)
        {
            StopCoroutine(attackCoroutine);
            attackCoroutine = null;
        }

        if (enemyList.Contains(this))
        {
            enemyList.Remove(this);
        }
    }

    private void OnDestroy()
    {
        healthComponent.OnDead -= Release;
        healthComponent.OnDead -= DropItems;
        healthComponent.OnDead -= SpawnDefeatedEffect;
    }

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        healthComponent = GetComponent<HealthComponent>();

        healthComponent.OnDead += Release;
        healthComponent.OnDead += DropItems;
        healthComponent.OnDead += SpawnDefeatedEffect;
    }

    private void FixedUpdate()
    {
        if (PlayerController.Instance != null)
        {
            Move();
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject == PlayerController.Instance)
            Attack();
    }

    private void DropItems()
    {
        foreach (var config in enemyStatus.dropItemList)
        {
            ObjectPoolManager.Instance.GetPooledObject(config.itemPrefab, transform.position);
        }
    }

    private void SpawnDefeatedEffect() => ObjectPoolManager.Instance.GetPooledObject(defeatedEffect, transform.position);

    protected abstract void Attack();
    protected abstract void Move();
}
