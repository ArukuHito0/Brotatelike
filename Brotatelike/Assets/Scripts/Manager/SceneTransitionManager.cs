using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using UnityEngine.SceneManagement;

public class SceneTransitionManager : MonoBehaviour
{
    [SerializeField] private Volume postProcessVolume;
    [SerializeField] private float fadeDuration;

    private ColorAdjustments colorAdjustments;
    private bool isTransitioning = false;

    private void Awake()
    {
        // プロファイルからColorAdjustmentsの設定を取得
        if (postProcessVolume.profile.TryGet(out colorAdjustments))
        {
            colorAdjustments.postExposure.value = 0f;
        }
    }

    public void OnLoadScendClicked(string sceneName)
    {
        if (isTransitioning) return;
        StartCoroutine(FadeOutAndLoad(sceneName));
    }

    public void OnQuitButtonClicked()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
        Application.Quit();
    }

    private IEnumerator FadeOutAndLoad(string sceneName)
    {
        isTransitioning = true;
        float elapsed = 0;

        while (elapsed < fadeDuration)
        {
            elapsed += Time.deltaTime;
            float t = elapsed / fadeDuration;

            if (colorAdjustments != null)
            {
                colorAdjustments.postExposure.value = Mathf.Lerp(0f, -10f, t * t);
            }

            yield return null;
        }

        SceneManager.LoadScene(sceneName);
    }
}
