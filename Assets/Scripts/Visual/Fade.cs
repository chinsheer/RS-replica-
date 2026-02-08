using System.Collections;
using UnityEngine;

public class Fade : MonoBehaviour
{
    private SpriteRenderer _spriteRenderer;

    public bool DestroyOnFadeComplete;

    void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void FadeOut(float duration)
    {
        StartCoroutine(FadeCoroutine(1f, 0f, duration));
    }

    public void FadeIn(float duration)
    {
        StartCoroutine(FadeCoroutine(0f, 1f, duration));
    }

    IEnumerator FadeCoroutine(float startAlpha, float endAlpha, float duration)
    {
        float elapsed = 0f;
        Color color = _spriteRenderer.color;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            float alpha = Mathf.Lerp(startAlpha, endAlpha, elapsed / duration);
            _spriteRenderer.color = new Color(color.r, color.g, color.b, alpha);
            yield return null;
        }

        _spriteRenderer.color = new Color(color.r, color.g, color.b, endAlpha);
        if (DestroyOnFadeComplete && endAlpha == 0f)
        {
            Destroy(gameObject);
        }
    }

}
