using UnityEngine;

public class BulletController : MonoBehaviour
{
    private PlayerController player;

    private float damage;
    private float moveSpeed;
    private float moveRange;
    private Vector3 targetDirection;

    [SerializeField] private string targetTag;

    public void Initialize(Vector3 dir, float spd, float dmg)
    {
        targetDirection = dir;
        moveSpeed = spd;
        damage = dmg;
    }

    private void Awake()
    {
        player = GameObject.Find("Player").GetComponent<PlayerController>();
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    private void Update()
    {
        moveRange += moveSpeed * Time.deltaTime;
        transform.position += targetDirection.normalized * moveSpeed * Time.deltaTime;
        if (moveRange > player.Range)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(targetTag))
        {
            IDamageable target = collision.GetComponent<IDamageable>();
            target.TakeDamage(damage);
            Destroy(gameObject);
        }
    }
}
