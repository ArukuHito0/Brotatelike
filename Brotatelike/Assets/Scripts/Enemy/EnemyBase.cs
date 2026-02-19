using ObjectPoolSystem;
using UnityEngine;
using System.Collections.Generic;

public abstract class EnemyBase : PooledObject, IDamageable
{
    public static List<EnemyBase> enemyList = new List<EnemyBase>();

    private ObjectPool expPool;

    private float currentHealth;
    [SerializeField] private float maxHealth;
    [SerializeField] private float moveSpeed;
    [SerializeField] private float power;
    public static PlayerController target;

    private void OnEnable()
    {
        currentHealth = maxHealth;
        enemyList.Add(this);
    }

    private void OnDisable()
    {
        if (enemyList.Contains(this))
        {
            enemyList.Remove(this);
        }
    }

    private void Awake()
    {
        expPool = GameObject.Find("ExpPool").GetComponent<ObjectPool>();
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

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            expPool.GetPooledObject().transform.position = transform.position;
            Release();
        }
    }
}
