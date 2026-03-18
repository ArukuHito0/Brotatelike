using ObjectPoolSystem;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public static PlayerController Instance { get; private set; }

    // プレイヤーのステータス系
    [SerializeField] private PlayerStatusData playerStatus;
    public PlayerStatusData PlayerStatus => playerStatus;
    public PlayerRuntimeStatus playerRuntimeStatus { get; private set; } = new PlayerRuntimeStatus();
     public Wallet wallet { get; private set; } = new Wallet();

    // 装備のインベントリ
    public WeaponInventory weaponInventory { get; private set; }
    public ItemInventory itemInventory { get; private set; }

    // 体力
    private HealthComponent healthComponent;
    public HealthComponent HealthComponent => healthComponent;

    // 経験値
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

        healthComponent = GetComponent<HealthComponent>();
        healthComponent.OnDead += () => gameObject.SetActive(false);

        expComponent = GetComponent<ExpComponent>();

        weaponInventory = new WeaponInventory(GameObject.Find("BulletPool").GetComponent<ObjectPool>());
        itemInventory = new ItemInventory();

        wallet.AddMoney(99999999);
    }

    private void Start()
    {
        healthComponent.SetHealth(playerRuntimeStatus.MaxHealth);
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

    public void StartAllWeaponCoroutine()
    {
        foreach (var weapon in weaponInventory.weaponList)
        {
            Debug.Log($"{weapon.GetWeaponData().Name}の攻撃サイクルを開始");
            weapon.StartAttack(this);
        }
    }

    public void StopAllWeapopnCoroutine()
    {
        foreach (var weapon in weaponInventory.weaponList)
        {
            Debug.Log($"{weapon.GetWeaponData().Name}の攻撃サイクルを停止");
            weapon.StopAttack(this);
        }
    }
}
