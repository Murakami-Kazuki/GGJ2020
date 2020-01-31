using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    public float sec;
    [SerializeField,Header("残り時間表示用Text")]
    private Text text;
    [SerializeField,Header("残り時間ゲージ")]
    private Image gage;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(CT_Timer());
    }


    /// <summary>
    /// タイマー処理
    /// </summary>
    IEnumerator CT_Timer()
    {
        for (float f = 0.0f; f < sec; f += 1.0f * Time.deltaTime)
        {
            gage.fillAmount = 1f - (f / sec);
            text.text = "" + (int)(sec - f);
            yield return null;
        }
    }
}
