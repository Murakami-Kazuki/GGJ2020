using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Title
{
    [RequireComponent(typeof(CanvasGroup))]
    public class PressKeyText : UIParts
    {
        [SerializeField] AnimationCurve flashCurve;
        [SerializeField] float flashDuration = 0.8f;

        CanvasGroup canvas;

        void Awake()
        {
            canvas = GetComponent<CanvasGroup>();
        }

        public void Show()
        {
            StartCoroutine(Flash(flashDuration));
        }

        IEnumerator Flash(float duration)
        {
            while (true)
            {
                UpdateAlpha(0, 1f, flashDuration);
                yield return new WaitForSeconds(flashDuration + 0.3f);
                UpdateAlpha(1f, 0f, flashDuration);
                yield return new WaitForSeconds(flashDuration);
            }
        }
    }
}