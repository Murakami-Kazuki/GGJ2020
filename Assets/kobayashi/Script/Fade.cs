using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using System;

namespace Title
{
    [RequireComponent(typeof(Image))]
    public class Fade : MonoBehaviour
    {
        Image myImage;
        [SerializeField] Color endColor;

        void Awake()
        {
            myImage = GetComponent<Image>();
        }

        /// <summary>
        //fade終了後にonfinishを呼ぶ
        //duration:fade時間(s)
        /// </summary>
        public void FadeIn(float duration, Action onFinish)
        {
            if (fadeCor != null) StopCoroutine(fadeCor);
            fadeCor = StartCoroutine(PlayFade(duration, onFinish));
        }

        Coroutine fadeCor;
        IEnumerator PlayFade(float duration, Action onFinish)
        {
            var timer = 0f;
            var rate = 0f;
            var startColor = endColor;
            startColor.a = 0;

            while (rate < 1f)
            {
                timer += Time.deltaTime;
                rate = Mathf.Clamp01(timer / duration);
                myImage.color = Color.Lerp(startColor, endColor, rate);
                yield return null;
            }
            myImage.color = endColor;

            onFinish();
        }

    }
}