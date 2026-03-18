using UnityEngine;
using ObjectPoolSystem;
using System.Collections;
using System.Diagnostics;

[System.Serializable]
public class Weapon
{
    public Weapon(GameObject owner, ObjectPool pool)
    {
        this.owner = owner;
        this.pool = pool;
    }

    private ObjectPool pool;

    private GameObject owner;
    
    [SerializeField] private WeaponData weaponData;

    public void SetWeaponData(WeaponData weaponData) => this.weaponData = weaponData;
    public WeaponData GetWeaponData() => this.weaponData;

    private Coroutine activeCoroutine;

    public void StartAttack(MonoBehaviour runner)
    {
        StopAttack(runner);
        activeCoroutine = runner.StartCoroutine(WeaponAttackCycle());
    }

    public void StopAttack(MonoBehaviour runner)
    {
        if (activeCoroutine != null)
        {
            runner.StopCoroutine(activeCoroutine);
            activeCoroutine = null;
        }
    }

    // •Ší‚ÌUŒ‚ƒTƒCƒNƒ‹ƒRƒ‹[ƒ`ƒ“
    private IEnumerator WeaponAttackCycle()
    {
        while (true)
        {
            // ƒN[ƒ‹ƒ^ƒCƒ€‚ğŠJn
            yield return new WaitForSeconds(weaponData.CoolTime);

            // “G‚ğ‘_‚¤ê‡‚ÍË’ö“à‚É“G‚ª‚­‚é‚Ü‚Å‘Ò‹@
            if (weaponData.isTargetting)
                yield return new WaitUntil(() => GetTarget.GetTargetInRange(EnemyBase.enemyList, owner.transform.position, weaponData.Range) != null);
            
            // ËŒ‚ƒRƒ‹[ƒ`ƒ“ŠJn
            yield return Shooting();
        }
    }

    // ËŒ‚ƒTƒCƒNƒ‹
    private IEnumerator Shooting()
    {
        if (weaponData.bulletCnt <= 0) yield break;

        // ËŒ‚•ûŒü‚ğİ’è
        var targetAngle = AngleOfBase();
                
        for (int i = 0; i < weaponData.bulletCnt; i++)
        {
            // ’e‚Ì”­Ë•ûŒü‚ğæ“¾
            float angle = AngleOfBullet(i);

            // ”­Ë•ûŒü‚ğƒ‰ƒWƒAƒ“‚É•ÏŠ·
            float rad = RadOfBullet(targetAngle, angle);

            // ³‹K‰»‚³‚ê‚½ƒxƒNƒgƒ‹‚É’e‚ğ‘Å‚¿o‚·
            ShotBullet(rad);

            if (weaponData.cycleTime != 0)
                yield return new WaitForSeconds(weaponData.fireRate);
        }
    }

    // •Ší‚Ì”­ËŠp“x‚ğæ“¾
    private float AngleOfBase()
    {
        var target = GetTarget.GetTargetInRange(EnemyBase.enemyList, owner.transform.position, weaponData.Range);

        // “G‚ğ‘_‚í‚È‚¢ê‡AŒÅ’è‚Ì”­ËŠp“x‚ğ•Ô‚·
        if(target == null || !weaponData.isTargetting) return weaponData.baseAngle;

        // •Ší‚Ìg—pÒ‚©‚ç‚Ìƒ^[ƒQƒbƒgˆÊ’u‚Ö‚ÌŠp“x‚ğ•Ô‚·
        return Mathf.Atan2(
            target.transform.position.y - owner.transform.position.y,
            target.transform.position.x - owner.transform.position.x
            ) * Mathf.Rad2Deg;
    }

    // ’e‚Ì”­ËŠp“x‚ğæ“¾
    private float AngleOfBullet(int num)
    {
        // ŠgUŠp“x‚ª0‚Ü‚½‚Í”­Ë‚·‚é’e”‚ª‚PˆÈ‰º‚Ìê‡A0“x‚ğ•Ô‚·
        if(weaponData.spreadAngle == 0 || weaponData.bulletCnt <= 1) return 0;

        // ŠgUŠp“x‚ª‘S•ûˆÊ‚Ìê‡AŠgUŠp“x‚ğ’e”‚Å‚»‚Ì‚Ü‚ÜŠ„‚Á‚½Šp“x‚É’e‚Ì”Ô†‚ğ‚©‚¯A‚»‚ÌŠp“x‚ğ•Ô‚·
        // ŠgUŠp“x‚ª‘S•ûˆÊ‚Å‚È‚¢ê‡AŠgUŠp“x‚ğ’e” -‚P‚ÅŠ„‚èo‚½Šp“x‚©‚çAŠgUŠp“x‚Ì”¼•ª‚ğˆø‚«A‚»‚ê‚É’e‚Ì”Ô†‚ğ‚©‚¯‚½Šp“x‚ğ•Ô‚·@¦îŒ`‚Ì^‚ñ’†‚ğ”­Ë•ûŒü‚É‚Á‚Ä‚­‚é‚½‚ß‚ÌŒvZ
        if (weaponData.spreadAngle == 360.0f)
            return (weaponData.spreadAngle / (float)weaponData.bulletCnt) * num;
        else
            return -(weaponData.spreadAngle / 2f) + (weaponData.spreadAngle / (float)(weaponData.bulletCnt - 1)) * num;
    }

    // •Ší‚Ì”­ËŠp“x‚Æ’e‚Ì”­ËŠp“x‚ğ‘«‚µ‚½Šp“x‚ğ•Ô‚·
    private float AngleOfShot(float shotAngle, float angle)
    {
        return shotAngle + angle;
    }

    // ’e‚Ì”­ËŠp“x‚ÉËŒ‚ƒGƒ‰[‚Ì•â³‚ğ‰Á‚¦‚½Šp“x‚ğ•Ô‚·
    private float AngleOfError(float angle)
    {
        float rndError = Random.Range(-weaponData.dispersion, weaponData.dispersion) / 100f * Mathf.Rad2Deg;

        if(weaponData.spreadAngle != 360)
            return Mathf.Clamp(angle + rndError, -(weaponData.spreadAngle / 2f), (weaponData.spreadAngle / 2));
        else
            return angle + rndError;
    }

    // ”­ËŠp“x‚ğƒ‰ƒWƒAƒ“‚É•ÏŠ·‚µ‚½³‹K‰»‚³‚ê‚½ƒxƒNƒgƒ‹‚ğ•Ô‚·
    private float RadOfBullet(float targetAngle, float angle)
    {
        if (weaponData.dispersion != 0)
            return AngleOfShot(targetAngle, AngleOfError(angle)) * Mathf.Deg2Rad;
        else
            return AngleOfShot(targetAngle, angle) * Mathf.Deg2Rad;
    }

    // ’e‚ÌˆÚ“®ƒxƒNƒgƒ‹‚ğ•Ô‚·
    private Vector3 BulletVelocity(float rad)
    {
        return new Vector2(Mathf.Cos(rad), Mathf.Sin(rad)) * weaponData.bulletSpeed;
    }

    // ’e‚ğ¶¬‚µA‚»‚ê‚ÉˆÚ“®ƒxƒNƒgƒ‹‚ğ“n‚·
    private void ShotBullet(float rad)
    {
        BulletController bullet = pool.GetPooledObject(owner.transform.position).GetComponent<BulletController>();
        bullet.Initialize(weaponData, BulletVelocity(rad));
    }
}
