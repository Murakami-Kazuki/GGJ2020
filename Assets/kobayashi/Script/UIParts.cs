using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class UIParts : MonoBehaviour
{

    protected IEnumerator DoCoroutine(float duration, AnimationCurve animationCurve, Action<AnimationCurve, float> action, Action onFinish = null)
    {
        var timer = 0f;
        var rate = 0f;

        while (rate < 1)
        {
            timer += Time.deltaTime;
            rate = Mathf.Clamp01(timer / duration);

            action.Invoke(animationCurve, rate);
            yield return null;
        }

        if (onFinish != null)
            onFinish();
    }
}
