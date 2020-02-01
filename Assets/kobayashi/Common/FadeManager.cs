using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeManager : MonoBehaviour
{

    protected static FadeManager instance;
    public static FadeManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = (FadeManager)FindObjectOfType(typeof(FadeManager));
                if (instance == null)
                {
                    var name = typeof(FadeManager).Name;
                    Debug.LogFormat("Create singleton object: {0}", name);
                    GameObject obj = new GameObject(name);
                    instance = obj.AddComponent<FadeManager>();
                }
            }
            return instance;
        }
    }


    [SerializeField] Image background;
    void Awake()
    {
        if (background == null)
        {
            var canvas = new GameObject("FadeCanvas", typeof(Canvas), typeof(CanvasScaler));
            canvas.GetComponent<Canvas>().renderMode = RenderMode.ScreenSpaceOverlay;
            canvas.GetComponent<CanvasScaler>().uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;


            var image = new GameObject("background", typeof(Image));
            background = image.GetComponent<Image>();
            background.transform.SetParent(canvas.transform);
            background.transform.localScale = Vector3.one;
            //background.GetComponent<RectTransform>().offsetMin = Vector2.zero;
            //background.GetComponent<RectTransform>().offsetMax = Vector2.zero;
            background.GetComponent<RectTransform>().anchorMin = new Vector2(0, 0);
            background.GetComponent<RectTransform>().anchorMax = new Vector2(1, 1);
            background.GetComponent<RectTransform>().pivot = new Vector2(0.5f, 0.5f);
            background.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
        }
        else
        {
            //background.gameObject.SetActive(false);
        }
    }

    Color startColor;
    Color endColor;

    void ChangeColor(AnimationCurve curve, float rate)
    {
        if (curve != null) rate = curve.Evaluate(rate);
        background.color = Color.Lerp(startColor, endColor, rate);
    }

    public void Fade(Color _startColor, Color _endColor, float duration, Action action = null)
    {
        startColor = _startColor;
        endColor = _endColor;
        background.gameObject.SetActive(true);
        background.enabled = true;
        if (cor != null) StopCoroutine(cor);
        cor = StartCoroutine(DoCoroutine(duration, null, ChangeColor, () =>
        {
            background.enabled = false;
            background.gameObject.SetActive(false);
            if (action != null) action();
        }));

    }

    //フェードをかける
    public void Fade(Color _endColor, float duration, Action action = null)
    {
        Fade(background.color, _endColor, duration, action);
    }

    Coroutine cor;
    IEnumerator DoCoroutine(float duration, AnimationCurve animationCurve, Action<AnimationCurve, float> action, Action onFinish = null)
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
