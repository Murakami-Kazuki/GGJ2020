using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AnnounceUI : Title.UIParts
{

    protected override void Awake()
    {
        base.Awake();
        UpdateAlpha(0);
    }

    [SerializeField] Text text;
    public void Show()
    {
        if (isShowing) return;
        text.text = GetAnnounceStr();
        if (cor != null) StopCoroutine(Showing());
        cor = StartCoroutine(Showing());
    }

    Coroutine cor;
    bool isShowing = false;
    IEnumerator Showing()
    {
        isShowing = true;
        MovePositionOnAnchor(Vector2.right * 1600);
        UpdateAlpha(1);
        MovePosition(null, Vector2.zero, 0.3f);
        yield return new WaitForSeconds(1f);
        MovePosition(null, Vector2.left * 1600, 0.3f);
        UpdateAlpha(1f, 0f, 0.4f);
        isShowing = false;
    }

    string GetAnnounceStr()
    {
        var announce = Dash.GameManager.Instance.CurrentAnnounceType;
        switch (announce)
        {
            case AnnounceType.SaraSara:
                return "麗しい髪が今のトレンドのようだ・・・";
            case AnnounceType.MojaMoja:
                return "いかめしい髪が今のトレンドのようだ・・・";
            case AnnounceType.GachiGachi:
                return "猛々しい髪が今のトレンドのようだ・・・";
            case AnnounceType.HadeHade:
                return "今年はとにかく派手なのがいいそうだ・・・";
        }

        return "";
    }
}
