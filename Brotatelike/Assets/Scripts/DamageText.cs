using System.Collections;
using TMPro;
using UnityEngine;

public class DamageText : PooledObject
{
    private TextMeshPro damageText;
    [SerializeField] private float animTime;
    [SerializeField] private float waitFadeTime;
    [SerializeField] private float fadeoutTime;

    private Color defaultColor;

    private void OnEnable()
    {
        damageText.color = defaultColor;
    }

    private void Awake()
    {
        damageText = GetComponent<TextMeshPro>();
        defaultColor = damageText.color;
    }

    public void SetDamageText(int damage, Vector3 pos)
    {
        damageText.transform.position = pos;
        damageText.text = damage.ToString();

        StartCoroutine(DamageTextAnim());
    }

    private IEnumerator DamageTextAnim()
    {
        float time = 0f;
        Vector3 startPos = transform.position;
        Vector3 endPos = transform.position + new Vector3(Random.Range(-1.5f, 1.5f), Random.Range(1, 1.5f));

        while (time < animTime)
        {
            time += Time.deltaTime;
            float t = time / animTime;

            transform.position = Vector3.Lerp(startPos, endPos, t);

            yield return null;
        }

        yield return new WaitForSeconds(waitFadeTime);

        time = 0;
        Color textColor = damageText.color;
        float startAlpha = textColor.a;

        while (time < fadeoutTime)
        {
            time += Time.deltaTime;
            float t = time / fadeoutTime;

            var alpha = Mathf.Lerp(startAlpha, 0, t);
            textColor.a = alpha;

            damageText.color = textColor;

            yield return null;
        }

        Release();
    }
}
