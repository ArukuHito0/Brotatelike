using Mono.Cecil.Cil;
using System.Collections;
using UnityEngine;

public class PlayerController : MonoBehaviour, IDamageable
{
    private Rigidbody2D rb;

    private float currentHealth;
    [SerializeField] private float maxHealth = 10;
    [SerializeField] private float moveSpeed = 5.0f;
    public float MoveSpeed => moveSpeed;
    [SerializeField] private float power = 1.0f;
    [SerializeField] private float range = 3.0f;
    public float Range => range;
    [SerializeField] private float bulletSpeed = 10.0f;
    [SerializeField] private float fireRate = 1.5f;
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Transform fieldSize;
    [SerializeField] private LayerMask targetLayer;

    private Vector3 targetPos = Vector3.zero;
    private Vector3 moveDir = Vector3.zero;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        currentHealth = maxHealth;
    }

    private void Start()
    {
        StartCoroutine(Shooter());
    }

    private void Update()
    {
        var x = Input.GetAxisRaw("Horizontal");
        var y = Input.GetAxisRaw("Vertical");
        moveDir = new Vector3(x, y, 0).normalized;
    }

    private void FixedUpdate()
    {
        var pos = transform.position + moveDir * moveSpeed * Time.fixedDeltaTime;
        pos.x = Mathf.Clamp(pos.x, -fieldSize.localScale.x * 0.5f + 1, fieldSize.localScale.x * 0.5f - 1);
        pos.y = Mathf.Clamp(pos.y, -fieldSize.localScale.y * 0.5f + 1, fieldSize.localScale.y * 0.5f - 1);
        rb.transform.position = pos;
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            Destroy(gameObject);
        }
    }

    private IEnumerator Shooter()
    {
        while (true)
        {
            yield return new WaitForSeconds(fireRate);

            GetTarget();

            if (targetPos != Vector3.zero)
            {
                BulletController bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity).GetComponent<BulletController>();
                bullet.Initialize(targetPos - transform.position, bulletSpeed, power);
            }
        }
    }

    private void GetTarget()
    {
        Collider2D[] targets = Physics2D.OverlapCircleAll(transform.position, range, targetLayer);
        targetPos = Vector3.zero;

        if (targets.Length <= 0 || targets == null)
        {
            return;
        }

        foreach (Collider2D target in targets)
        {
            if (targetPos == Vector3.zero)
            {
                targetPos = target.transform.position;
                return;
            }
            else
            {
                targetPos = Vector3.Distance(targetPos, transform.position) < Vector3.Distance(target.transform.position, transform.position) ?
                            targetPos : target.transform.position;
            }
        }
    }
}
