using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class Koma : BaseKoma
{
    [SerializeField] AnimationCurve movementCurve;
    [SerializeField] bool hasDuration;

    public override IEnumerator Show()
    {
        //Debug.Log("show koma");
        GetComponent<Image>().enabled = true;
        var pos = MyAnchoredPos;
        MovePositionOnAnchor(pos - Vector2.right * 500);
        MovePosition(movementCurve, pos, 0.4f);
        UpdateAlpha(0f, 1f, 0.3f);

        if (hasDuration) yield return new WaitForSeconds(1f);
        else yield return null;
    }
}
