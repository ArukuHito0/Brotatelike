using ObjectPoolSystem;
using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using Unity.VisualScripting.Antlr3.Runtime.Misc;

public abstract class EnemyBase : PooledObject
{
    public static List<EnemyBase> enemyList = new List<EnemyBase>();

    public EnemyStatusData enemyStatus { get; private set; }

    private ObjectPool expPool;
    private ObjectPool particlePool;

    private HealthComponent healthComponent;

    private Coroutine attackCoroutine = null;

    protected override void OnSpawn()
    {
        enemyList.Add(this);
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
        healthComponent.OnDead -= () => expPool.GetPooledObject(transform.position);
        healthComponent.OnDead += () => particlePool.GetPooledObject(transform.position);
    }

    private void Awake()
    {
        expPool = GameObject.Find("ExpPool").GetComponent<ObjectPool>();
        particlePool = GameObject.Find("DefeatedParticlePool").GetComponent<ObjectPool>();

        healthComponent = GetComponent<HealthComponent>();

        healthComponent.OnDead += Release;
        healthComponent.OnDead += () => expPool.GetPooledObject(transform.position);
        healthComponent.OnDead += () => particlePool.GetPooledObject(transform.position);
    }

    private void Update()
    {
        if (PlayerController.Instance != null)
        {
            Move();
        }
    }

    public void Initialize(EnemyStatusData statusData)
    {
        enemyStatus = statusData;
        GetComponent<SpriteRenderer>().sprite = enemyStatus.EnemySprite;
        healthComponent.SetHealth(statusData.MaxHealth);
        attackCoroutine = StartCoroutine(AttackCoroutine(PlayerController.Instance.HealthComponent));
    }

    public virtual IEnumerator AttackCoroutine(HealthComponent target) { yield return null; }

    public virtual void Move() { }
}
