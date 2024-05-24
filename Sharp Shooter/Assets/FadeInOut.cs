using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class FadeInOut : MonoBehaviour
{
    public List<SpriteRenderer> spriteRenderers;
    public List<TextMeshProUGUI> textMeshProUGUIs;
    public float fadeDuration = 1.0f;  // Duración del fade

    private float fadeTimer;
    private float startAlpha;
    private float endAlpha;
    private bool isFading;
    private bool deactivateOnEnd;

    private float fTime;
    // Función pública para iniciar el fade in
    public void FadeIn(float fadeStart, float fadeTime)
    {
        fTime = fadeTime;
        Invoke("FadeInR", fadeStart);
    }

    // Función pública para iniciar el fade out
    public void FadeOut(float fadeTime)
    {
        fadeDuration = fadeTime;
        StartFade(1f, 0f, true);
    }

    private void FadeInR()
    {
        fadeDuration = fTime;
        StartFade(0f, 1f, false);
    }

    private void StartFade(float startAlpha, float endAlpha, bool deactivateOnEnd)
    {
        this.startAlpha = startAlpha;
        this.endAlpha = endAlpha;
        this.deactivateOnEnd = deactivateOnEnd;
        isFading = true;
        fadeTimer = 0f;
    }

    void Update()
    {
        if (isFading)
        {
            fadeTimer += Time.deltaTime;
            float alpha = Mathf.Lerp(startAlpha, endAlpha, fadeTimer / fadeDuration);

            // Ajustar la opacidad de todos los SpriteRenderers
            foreach (var spriteRenderer in spriteRenderers)
            {
                Color spriteColor = spriteRenderer.color;
                spriteRenderer.color = new Color(spriteColor.r, spriteColor.g, spriteColor.b, alpha);
            }

            // Ajustar la opacidad de todos los TextMeshProUGUIs
            foreach (var textMeshPro in textMeshProUGUIs)
            {
                Color textColor = textMeshPro.color;
                textMeshPro.color = new Color(textColor.r, textColor.g, textColor.b, alpha);
            }

            if (fadeTimer >= fadeDuration)
            {
                isFading = false;
                fadeTimer = 0f;

                // Asegurar que el alpha final sea exactamente el endAlpha
                foreach (var spriteRenderer in spriteRenderers)
                {
                    Color spriteColor = spriteRenderer.color;
                    spriteRenderer.color = new Color(spriteColor.r, spriteColor.g, spriteColor.b, endAlpha);
                }

                foreach (var textMeshPro in textMeshProUGUIs)
                {
                    Color textColor = textMeshPro.color;
                    textMeshPro.color = new Color(textColor.r, textColor.g, textColor.b, endAlpha);
                }

                if (deactivateOnEnd)
                {
                    gameObject.SetActive(false);
                }
            }
        }
    }
}
