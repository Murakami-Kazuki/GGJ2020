using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CanvasGroup))]
public class PressKeyText : UIParts
{
    [SerializeField] AnimationCurve flashCurve;
    [SerializeField] float flashDuration;

    CanvasGroup canvas;

    void Start()
    {
        canvas = GetComponent<CanvasGroup>();
        StartCoroutine(Flash(flashDuration));
    }

    IEnumerator Flash(float duration)
    {
        while (true)
        {
            UpdateAlpha(0, 1f, flashDuration);
            yield return new WaitForSeconds(flashDuration + 0.2f);
            UpdateAlpha(1f, 0f, flashDuration);
            yield return new WaitForSeconds(flashDuration);
        }
    }

    float startAlpha;
    float endAlpha;
    void UpdateAlpha(AnimationCurve curve, float rate)
    {
        rate = curve.Evaluate(rate);
        canvas.alpha = Mathf.Lerp(startAlpha, endAlpha, rate);
    }

    void UpdateAlpha(float sAlpha, float eAlpha, float duration)
    {
        startAlpha = sAlpha;
        endAlpha = eAlpha;
        StartCoroutine(DoCoroutine(duration, flashCurve, UpdateAlpha));
    }
}
