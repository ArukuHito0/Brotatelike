using Mono.Cecil.Cil;
using ObjectPoolSystem;
using System;
using System.Collections;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Video;

public class PlayerController : MonoBehaviour
{
    public static PlayerController Instance { get; private set; }
    [SerializeField] private PlayerStatusData playerStatus;
    public PlayerStatusData PlayerStatus => playerStatus;
    public PlayerRuntimeStatus playerRuntimeStatus { get; private set; } = new PlayerRuntimeStatus();

    private ObjectPool bulletPool;

    private HealthComponent healthComponent;
    public HealthComponent HealthComponent => healthComponent;
    private ExpComponent expComponent;
    public ExpComponent ExpComponent => expComponent;

    public bool IsDead => healthComponent.IsDead;

    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Transform fieldSize;
    [SerializeField] private LayerMask targetLayer;

    private Vector3 moveDir = Vector3.zero;

    private void OnDestroy()
    {
        Instance = null;

        healthComponent.OnDead -= () => gameObject.SetActive(false);
    }

    private void Awake()
    {
        Application.targetFrameRate = 60;

        Instance = this;

        bulletPool = GameObject.Find("BulletPool").GetComponent<ObjectPool>();
        
        healthComponent = GetComponent<HealthComponent>();
        healthComponent.OnDead += () => gameObject.SetActive(false);

        expComponent = GetComponent<ExpComponent>();
    }

    private void Start()
    {
        healthComponent.SetHealth(playerRuntimeStatus.MaxHealth);

        StartCoroutine(Shooter());
        StartCoroutine(CollectItem());
    }

    private void Update()
    {
        var x = Input.GetAxisRaw("Horizontal");
        var y = Input.GetAxisRaw("Vertical");
        moveDir = new Vector3(x, y, 0).normalized;

        var pos = transform.position + moveDir * playerRuntimeStatus.MoveSpeed * Time.deltaTime;
        pos.x = Mathf.Clamp(pos.x, -fieldSize.localScale.x * 0.5f + 1, fieldSize.localScale.x * 0.5f - 1);
        pos.y = Mathf.Clamp(pos.y, -fieldSize.localScale.y * 0.5f + 1, fieldSize.localScale.y * 0.5f - 1);
        transform.position = pos;
    }

    private IEnumerator Shooter()
    {
        while (true)
        {
            EnemyBase target = GetTarget.GetTargetInRange(EnemyBase.enemyList, transform.position, playerRuntimeStatus.AttackRange);

            if (target != null)
            {
                BulletController bullet = bulletPool.GetPooledObject(transform.position).GetComponent<BulletController>();
                bullet.Initialize((target.transform.position - bullet.transform.position).normalized, 10, playerRuntimeStatus.Strength);
            }

            yield return new WaitForSeconds(playerRuntimeStatus.AttackSpeed);
        }
    }

    private IEnumerator CollectItem()
    {
        while (true)
        {
            GetTarget.GetTargetInRange(PickableItem.itemList, transform.position, playerRuntimeStatus.CollectRange)?.PickUp(this);

            yield return null;
        }
    }
}
