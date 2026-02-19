using Mono.Cecil.Cil;
using ObjectPoolSystem;
using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Video;

public class PlayerController : MonoBehaviour, IDamageable
{
    private Rigidbody2D rb;
    private ObjectPool bulletPool;

    private int currentLevel = 1;
    private int exp = 0;
    private int levelUpExp = 100;
    [SerializeField]
    private TextMeshProUGUI currentLevelText;
    [SerializeField]
    private GaugeUIBar expBar;

    [SerializeField]
    private GaugeUIBar healthBar;
    [SerializeField]
    private TextMeshProUGUI healthText;
    private int currentHealth;
    [SerializeField] private int maxHealth;

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

    private Vector3 moveDir = Vector3.zero;

    public bool IsDead => currentHealth <= 0;

    public static Action OnDeadPlayer;

    private void OnDisable()
    {
        OnDeadPlayer -= Dead;
        EnemyBase.target = null;
    }

    private void Awake()
    {
        Application.targetFrameRate = 60;

        EnemyBase.target = this;

        rb = GetComponent<Rigidbody2D>();
        bulletPool = GameObject.Find("BulletPool").GetComponent<ObjectPool>();
        currentHealth = maxHealth;

        OnDeadPlayer += Dead;

        healthBar?.UpdateFillAmount(GetHealthRate());
        expBar?.UpdateFillAmount(0);
        currentLevelText.text = $"Lv.<size=40>{currentLevel}";
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

        if (Input.GetKeyDown(KeyCode.E))
        {
            TakeDamage(20);
        }
        if (Input.GetKeyDown(KeyCode.Q))
        {
            Heal(20);
        }

        var pos = transform.position + moveDir * moveSpeed * Time.deltaTime;
        pos.x = Mathf.Clamp(pos.x, -fieldSize.localScale.x * 0.5f + 1, fieldSize.localScale.x * 0.5f - 1);
        pos.y = Mathf.Clamp(pos.y, -fieldSize.localScale.y * 0.5f + 1, fieldSize.localScale.y * 0.5f - 1);
        transform.position = pos;
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= (int)damage;
        healthText.text = $"{currentHealth} <size=15>/ {maxHealth}";

        if (IsDead)
        {
            OnDeadPlayer?.Invoke();
        }

        healthBar?.UpdateFillAmount(GetHealthRate());
    }

    public void Heal(float amount)
    {
        currentHealth = Mathf.Clamp(currentHealth + (int)amount, 0, maxHealth);
        healthText.text = $"{currentHealth} <size=15>/ {maxHealth}";
        healthBar?.UpdateFillAmount(GetHealthRate());
    }

    private void Dead()
    {
        OnDeadPlayer -= Dead;

        healthBar?.UpdateFillAmount(0);

        gameObject.SetActive(false);
    }

    private float GetHealthRate()
    {
        return (float)currentHealth / maxHealth;
    }

    private IEnumerator Shooter()
    {
        while (true)
        {
            var target = GetTarget.GetTargetInRange(transform.position, range);

            if (target != null)
            {
                BulletController bullet = bulletPool.GetPooledObject().GetComponent<BulletController>();
                bullet.transform.position = transform.position;
                bullet.Initialize((target.transform.position - bullet.transform.position).normalized, bulletSpeed, power);
            }

            yield return new WaitForSeconds(fireRate);
        }
    }

    public void AddExp(int amount)
    {
        exp += amount;

        if (exp >= levelUpExp)
        {
            var e = exp - levelUpExp;
            exp = e <= 0 ? 0 : e;
            LevelUp();
        }

        expBar?.UpdateFillAmount((float)exp / levelUpExp);
    }

    public void LevelUp()
    {
        currentLevel++;
        currentLevelText.text = $"Lv.<size=40>{currentLevel}";
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        IPickable pick = collision.GetComponent<IPickable>();
        pick?.PickUp();
    }
}
