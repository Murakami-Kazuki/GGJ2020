using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

public class BaseKoma : MonoBehaviour
{

    public virtual IEnumerator Show()
    {
        Debug.Log("show koma");
        GetComponent<Image>().enabled = true;
        yield return new WaitForSeconds(0.2f);
    }
}
