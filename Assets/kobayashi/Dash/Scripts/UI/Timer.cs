using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Dash
{
    public class Timer : MonoBehaviour
    {
        [SerializeField] float duration;
        [SerializeField] Text text;
        float timer = 0;


        public void StartTimer()
        {
            StartCoroutine(Countdown());
        }


        IEnumerator Countdown()
        {
            timer = duration;
            while (0 < timer)
            {
                timer -= Time.deltaTime;
                UpdateText();
                yield return null;
            }

            text.text = "00:00:00";
        }

        void UpdateText()
        {
            text.text = GetStr();
        }

        string GetStr()
        {
            var integer = Mathf.Round(timer);
            var few = timer - Mathf.Floor(timer);
            var miri = Mathf.Round(few * 10 * 10);
            few = Mathf.Floor(timer * Mathf.Pow(10, 4)) - Mathf.Floor(timer * Mathf.Pow(10, 4));
            //Debug.Log("few:" + few + " miri:" + miri);
            var micro = Random.Range(0, 99);// Mathf.Round(few * 10 * 10);

            return integer.ToString("00") + ":" + miri.ToString("00") + ":" + micro.ToString("00");
        }

    }
}