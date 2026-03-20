using UnityEngine;

public class DamageTextGenerator : MonoBehaviour
{
    [SerializeField] private DamageText damageTextPrefab;

    private void OnEnable()
    {
        HealthComponent.OnDamaged += DisplayDamage;
    }

    private void OnDisable()
    {
        HealthComponent.OnDamaged -= DisplayDamage;
    }

    private void DisplayDamage(Vector3 pos, int damage)
    {
        ObjectPoolManager.Instance.GetPooledObject(damageTextPrefab, pos).GetComponent<DamageText>().SetDamageText(damage);
    }
}
