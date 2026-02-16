using UnityEngine;

public abstract class EnemyBase : PooledObject, IDamageable
{
    private float currentHealth;
    [SerializeField] private float maxHealth;
    [SerializeField] private float moveSpeed;
    [SerializeField] private float power;
    private GameObject target;

    private void OnEnable()
    {
        currentHealth = maxHealth;
    }

    private void Awake()
    {
        target = GameObject.Find("Player");
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
            Release();
        }
    }
}
