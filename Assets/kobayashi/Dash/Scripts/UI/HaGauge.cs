using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

namespace Dash
{
    public class HaGauge : Title.UIParts
    {
        [SerializeField] protected Image fillArea;
        protected float minValue = 0f;
        protected float maxValue = 1f;
        public float FillAmount
        {
            get { return fillArea.fillAmount; }
        }

        public void ResetFillAmount()
        {
            fillArea.fillAmount = 0;
        }

        public void UpdateFillAmount(float rate)
        {
            fillArea.fillAmount = rate;
        }

        //ゲージによってたまるlevel
        public int GaugeLevel
        {
            get
            {
                if (fillArea.fillAmount < 0.33f) return 1;
                if (fillArea.fillAmount < 0.66f) return 2;

                return 3;
            }
        }

        float startFillAmount;
        float endFillAmount;
        protected Coroutine fillCor;
        void UpdateFillAmount(AnimationCurve animationCurve, float currentRate)
        {
            var animationRate = currentRate;
            if (animationCurve != null) animationRate = animationCurve.Evaluate(currentRate);
            var amount = Mathf.Lerp(startFillAmount, endFillAmount, animationRate);
            UpdateFillAmount(amount);
        }

        public void UpdateFillAmount(float idealFillAmount, float duration, Action onFinish = null)
        {
            startFillAmount = fillArea.fillAmount;
            endFillAmount = Mathf.Clamp01(idealFillAmount);
            if (startFillAmount == endFillAmount) return;
            if (duration <= 0) duration = 0.01f;
            if (fillCor != null) StopCoroutine(fillCor);
            fillCor = StartCoroutine(DoCoroutine(duration, null, UpdateFillAmount, onFinish));
        }

        //もしアクションを使う場合
        public void UpdatePowerCallback(float currentRate)
        {
            UpdateFillAmount(currentRate, 0.2f);
        }
    }
}
