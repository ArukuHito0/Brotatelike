using UnityEngine;

public class EnemyController : MonoBehaviour, IDamageable
{
    Rigidbody2D rb;
    private float currentHealth;
    [SerializeField] private float maxHealth = 1;
    [SerializeField] private float moveSpeed = 2.5f;
    [SerializeField] private float power = 1.0f;
    private GameObject target;
    private Vector3 TargetPos => target.transform.position;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        target = GameObject.Find("Player");
        currentHealth = maxHealth;
    }

    private void Start()
    {
        
    }

    private void FixedUpdate()
    {
        rb.linearVelocity = (TargetPos - transform.position).normalized * moveSpeed;
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            Destroy(gameObject);
        }
    }
}
