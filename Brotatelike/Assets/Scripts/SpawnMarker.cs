using System;
using System.Collections;
using UnityEngine;

public class SpawnMarker : PooledObject
{
    private SpriteRenderer spriteRenderer;
    [SerializeField] private float elapsedTime;
    [SerializeField] private float fadeTime;
    [SerializeField] private AnimationCurve easeCurve = AnimationCurve.EaseInOut(0, 0, 1, 1);

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        StartCoroutine(AssertionAnimation());
    }

    public IEnumerator AssertionAnimation()
    {
        float time = 0;
        Color color = spriteRenderer.color;
        float firstAlpha = spriteRenderer.color.a;

        while (time < elapsedTime)
        {
            float _t = time / elapsedTime;

            if (time < fadeTime)
            {
                time += Time.unscaledDeltaTime;
                float t = time / fadeTime;
                float curveT = easeCurve.Evaluate(t);

                float alpha = Mathf.Lerp(firstAlpha, 0.2f, t * _t);
                color.a = alpha;
                spriteRenderer.color = color;
            }
            else
            {
                time -= Time.unscaledDeltaTime;
                float t = time / fadeTime;
                float curveT = easeCurve.Evaluate(t);

                float alpha = Mathf.Lerp(firstAlpha, 0.2f, t * _t);
                color.a = alpha;
                spriteRenderer.color = color;
            }

            yield return null;
        }

        Release();
    }
}
