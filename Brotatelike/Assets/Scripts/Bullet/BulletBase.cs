using UnityEngine;

public abstract class BulletBase : PooledObject, IBullet
{
    private WeaponData weaponData;

    private Vector3 velocity;
    private Vector3 startPos;

    [SerializeField] private string targetTag;
    
    public abstract void OnHit();

    protected override void OnSpawn()
    {
        startPos = transform.position;
    }

    private void Update()
    {
        transform.position += velocity * Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(targetTag))
        {
            OnHit();
        }
    }
}
