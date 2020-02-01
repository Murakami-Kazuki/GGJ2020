using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Result
{
    public class ResultCanvas : MonoBehaviour
    {
        [SerializeField] HairResult hairResult;
        [SerializeField] List<Text> pointList;
        [SerializeField] Text resultText;

        [SerializeField] Image background;

        void Start()
        {
            background.gameObject.SetActive(false);
            //ShowResult(AnnounceType.MojaMoja);
            FadeManager.Instance.Fade(Color.white, new Color(1, 1, 1, 0), 1f, true);
        }

        /// <summary>
        // リザルト画面を表示
        /// </summary>
        public void ShowResult(AnnounceType type)
        {
            var dict = hairResult.MyHairDict;
            pointList[0].text = dict[HairType.SaraSara].ToString();
            pointList[1].text = dict[HairType.GachiGachi].ToString();
            pointList[2].text = dict[HairType.MojaMoja].ToString();

            resultText.text = hairResult.GetResult(type);
            StartCoroutine(Show());
        }

        IEnumerator Show()
        {
            //TODO アニメーションを追加
            background.gameObject.SetActive(true);
            yield return null;
        }
    }
}