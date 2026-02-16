using Mono.Cecil.Cil;
using ObjectPoolSystem;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Video;

public class PlayerController : MonoBehaviour, IDamageable
{
    private Rigidbody2D rb;
    private ObjectPool bulletPool;

    private float currentHealth;
    [SerializeField] private float maxHealth;

    [SerializeField] private float moveSpeed;
    public float MoveSpeed => moveSpeed;

    [SerializeField] private float power;
    [SerializeField] private float range;
    public float Range => range;
    [SerializeField] private float bulletSpeed;
    [SerializeField] private float fireRate;

    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Transform fieldSize;
    [SerializeField] private LayerMask targetLayer;

    private Vector3 targetPos = Vector3.zero;
    private Vector3 moveDir = Vector3.zero;

    public bool IsDead => (int)currentHealth <= 0;

    public static Action OnDeadPlayer;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        bulletPool = GameObject.Find("BulletPool").GetComponent<ObjectPool>();
        currentHealth = maxHealth;

        OnDeadPlayer += Dead;
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

        if (Input.GetMouseButton(1))
        {
            TakeDamage(10 * Time.deltaTime);
        }

        var pos = transform.position + moveDir * moveSpeed * Time.deltaTime;
        pos.x = Mathf.Clamp(pos.x, -fieldSize.localScale.x * 0.5f + 1, fieldSize.localScale.x * 0.5f - 1);
        pos.y = Mathf.Clamp(pos.y, -fieldSize.localScale.y * 0.5f + 1, fieldSize.localScale.y * 0.5f - 1);
        transform.position = pos;
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        if (IsDead)
        {
            OnDeadPlayer?.Invoke();
        }

        HealthBar.OnUpdateHealthBar?.Invoke(GetHealthRate());
    }

    private void Dead()
    {
        OnDeadPlayer -= Dead;

        gameObject.SetActive(false);
    }

    private float GetHealthRate()
    {
        return currentHealth / maxHealth;
    }

    private IEnumerator Shooter()
    {
        while (true)
        {
            yield return new WaitForSeconds(fireRate);

            GetTarget();

            if (targetPos != Vector3.zero)
            {
                BulletController bullet = bulletPool.GetPooledObject().GetComponent<BulletController>();
                bullet.transform.position = transform.position;
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
