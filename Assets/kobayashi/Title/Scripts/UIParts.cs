using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace Title
{
    public class UIParts : MonoBehaviour
    {

        protected RectTransform rectTrans;
        protected Vector2 MyAnchoredPos
        {
            get { return rectTrans.anchoredPosition; }
        }

        protected virtual void Awake()
        {
            rectTrans = GetComponent<RectTransform>();
        }

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

        float startAlpha;
        float endAlpha;
        CanvasGroup canvasGroup;
        void UpdateAlpha(AnimationCurve curve, float rate)
        {
            if (curve != null) rate = curve.Evaluate(rate);
            canvasGroup.alpha = Mathf.Lerp(startAlpha, endAlpha, rate);
        }

        protected void UpdateAlpha(float alpha)
        {
            if (canvasGroup == null) canvasGroup = GetComponent<CanvasGroup>();
            canvasGroup.alpha = alpha;
        }

        protected void UpdateAlpha(float sAlpha, float eAlpha, float duration)
        {
            if (canvasGroup == null) canvasGroup = GetComponent<CanvasGroup>();
            startAlpha = sAlpha;
            endAlpha = eAlpha;
            StartCoroutine(DoCoroutine(duration, null, UpdateAlpha));
        }
        protected void UpdateAlpha(float sAlpha, float eAlpha, float duration, AnimationCurve curve)
        {
            if (canvasGroup == null) canvasGroup = GetComponent<CanvasGroup>();
            startAlpha = sAlpha;
            endAlpha = eAlpha;
            StartCoroutine(DoCoroutine(duration, curve, UpdateAlpha));
        }

        Vector2 startPos;
        Vector2 endPos;
        Coroutine movementCor;
        void MovePosition(AnimationCurve animationCurve, float currentRate)
        {
            if (animationCurve != null) currentRate = animationCurve.Evaluate(currentRate);
            rectTrans.anchoredPosition = Vector2.Lerp(startPos, endPos, currentRate);
        }

        public void MovePosition(AnimationCurve animationCurve, Vector2 idealPos, float duration, Action onFinish = null)
        {
            startPos = rectTrans.anchoredPosition;
            endPos = idealPos;
            if (duration <= 0) duration = 0.001f;
            if (movementCor != null) StopCoroutine(movementCor);
            movementCor = StartCoroutine(DoCoroutine(duration, animationCurve, MovePosition, onFinish));
        }
        public void MovePositionOnAnchor(Vector2 idealPos)
        {
            rectTrans.anchoredPosition = idealPos;
        }
    }
}