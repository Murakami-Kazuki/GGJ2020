using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KomaControl : MonoBehaviour
{

    [SerializeField] List<BaseKoma> komaList;

    [SerializeField] Title.PressKeyText pressKeyText;

    void Start()
    {
        StartCoroutine(ShowKomaList());
    }

    IEnumerator ShowKomaList()
    {
        for (int n = 0; n < komaList.Count; n++)
        {
            yield return komaList[n].Show();
        }

        yield return null;
        pressKeyText.Show();

        StartCoroutine(WaitForTap());
    }


    IEnumerator WaitForTap()
    {
        var isTapped = false;
        while (!isTapped)
        {
            if (IsPressButton())
            {
                TransNextScene();
                isTapped = true;
            }

            yield return null;
        }

    }

    bool IsPressButton()
    {
        //TODO 入力受付
        if (Input.GetMouseButtonDown(0)) return true;
        return false;
    }

    void TransNextScene()
    {
        Debug.Log("ok. trans next scene.");

        //TODO trans next scene
    }
}
