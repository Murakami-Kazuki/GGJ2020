using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScissorsKoma : BaseKoma
{
    [SerializeField] AnimationCurve movementCurve;

    public override IEnumerator Show()
    {
        //Debug.Log("show koma");
        GetComponent<Image>().enabled = true;
        var pos = MyAnchoredPos;
        MovePositionOnAnchor(pos + new Vector2(200, 500));
        MovePosition(movementCurve, pos, 0.5f);
        UpdateAlpha(0f, 1f, 0.3f);
        yield return new WaitForSeconds(1f);
    }
}
