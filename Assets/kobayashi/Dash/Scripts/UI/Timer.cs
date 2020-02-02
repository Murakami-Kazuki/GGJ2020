using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Dash
{
    public class Timer : MonoBehaviour
    {
        float duration;
        [SerializeField] Text text;
        float timer = 0;

        bool isAnnounce = false;

        [SerializeField]
        private GameObject[] timerNumObject;


        public void StartTimer()
        {
            duration = GameManager.Instance.GameDuration;
            StartCoroutine(Countdown());
        }


        IEnumerator Countdown()
        {
            timer = duration;
            while (0 < timer)
            {
                timer -= Time.deltaTime;
                if (!isAnnounce && timer <= 15f)
                {
                    isAnnounce = true;
                    GameManager.Instance.Announce();
                }
                UpdateText();
                yield return null;
            }

            text.text = "00:00:00";

            GameManager.Instance.FinishGame();
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
            SetNum(timerNumObject[0], int.Parse(integer.ToString("00").Substring(0, 1)));
            SetNum(timerNumObject[1], int.Parse(integer.ToString("00").Substring(1, 1)));

            SetNum(timerNumObject[2], int.Parse(miri.ToString("00").Substring(0, 1)));
            SetNum(timerNumObject[3], int.Parse(miri.ToString("00").Substring(1, 1)));

            SetNum(timerNumObject[4], int.Parse(micro.ToString("00").Substring(0, 1)));
            SetNum(timerNumObject[5], int.Parse(micro.ToString("00").Substring(1, 1)));



            return integer.ToString("00") + ":" + miri.ToString("00") + ":" + micro.ToString("00");
        }




        private void SetNum(GameObject numObject, int num)
        {
            if (num > 10) num -= 10;
            int count = 0;
            foreach (Transform child in numObject.transform)
            {
                if (count == num)
                {
                    child.gameObject.SetActive(true);
                }
                else
                {
                    child.gameObject.SetActive(false);
                }

                count++;
            }

        }

    }
}