using ObjectPoolSystem;
using UnityEngine;
using System.Collections.Generic;

public abstract class EnemyBase : PooledObject
{
    public static List<EnemyBase> enemyList = new List<EnemyBase>();

    private ObjectPool expPool;

    private HealthComponent healthComponent;

    [SerializeField] private float moveSpeed;
    [SerializeField] private float power;
    public static PlayerController target;

    private void OnEnable()
    {
        enemyList.Add(this);
    }

    private void OnDisable()
    {
        if (enemyList.Contains(this))
        {
            enemyList.Remove(this);
        }
    }

    private void OnDestroy()
    {
        healthComponent.OnDead -= Release;
        healthComponent.OnDead -= () => expPool.GetPooledObject().transform.position = transform.position;
    }

    private void Awake()
    {
        expPool = GameObject.Find("ExpPool").GetComponent<ObjectPool>();

        healthComponent = GetComponent<HealthComponent>();
        healthComponent.OnDead += Release;
        healthComponent.OnDead += () => expPool.GetPooledObject().transform.position = transform.position;
    }

    private void Start()
    {
        
    }

    private void Update()
    {
        if (target != null)
        {
            transform.position += (target.transform.position - transform.position).normalized * moveSpeed * Time.deltaTime;
        }
    }
}
