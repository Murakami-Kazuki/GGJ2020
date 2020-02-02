using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerCanvas : MonoBehaviour
{
    [SerializeField] Title.UIParts image;

    [SerializeField, Header("up 系")] Sprite gachigachiUp;
    [SerializeField] Sprite sarasaraUp;
    [SerializeField] Sprite mojamojaUp;

    [SerializeField, Header("down 系")] Sprite gachigachiDown;
    [SerializeField] Sprite sarasaraDown;
    [SerializeField] Sprite mojamojaDown;

    [Header("アニメーションで使用するもの")]
    [SerializeField] AnimationCurve showCurve;

    public void GetHair(HairType type)
    {
        switch (type)
        {
            case HairType.GachiGachi: image.GetComponent<Image>().sprite = gachigachiUp; break;
            case HairType.SaraSara: image.GetComponent<Image>().sprite = sarasaraUp; break;
            case HairType.MojaMoja: image.GetComponent<Image>().sprite = mojamojaUp; break;
        }

        image.gameObject.SetActive(true);
        image.MovePositionOnAnchor(Vector2.zero);
        image.MovePosition(showCurve, Vector2.up * 3, 0.3f, () =>
        {
            image.MovePosition(null, Vector2.up * 3, 0.3f, () =>
            {
                image.gameObject.SetActive(false);
            });
        });
        image.UpdateAlpha(0, 1f, 0.15f);
    }
}
