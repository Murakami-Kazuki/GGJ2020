using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Dash
{
    public class StartPanel : BasePanel
    {
        [SerializeField] Text countdownText;

        public override void Activate()
        {
            base.Activate();

            StartCoroutine(Countdown(3));
        }

        IEnumerator Countdown(int count)
        {
            while (0 < count)
            {
                countdownText.text = count.ToString();
                yield return new WaitForSeconds(1f);
                count--;
            }

            countdownText.text = "0";

        }
    }
}
