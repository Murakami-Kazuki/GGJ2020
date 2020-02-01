using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class KomaControl : MonoBehaviour
{

    [SerializeField] List<BaseKoma> komaList;

    [SerializeField] Title.PressKeyText pressKeyText;

    void Start()
    {
        pressKeyText.GetComponent<CanvasGroup>().alpha = 0;
        StartCoroutine(ShowKomaList());
    }

    IEnumerator ShowKomaList()
    {
        yield return new WaitForSeconds(1f);
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
        if (Input.GetButtonDown("DS4square") || Input.GetKey(KeyCode.A))
        {
            return true;
        }
        if (Input.GetMouseButtonDown(0)) return true;
        return false;
    }

    void TransNextScene()
    {
        Debug.Log("ok. trans next scene.");

        FadeManager.Instance.Fade(new Color(1, 1, 1, 0), Color.white, 0.4f, false, () =>
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene("gameScene");
            //TODO trans next scene
        });

    }
}
