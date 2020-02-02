using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class statusUI : MonoBehaviour
{
    private GameObject fadeObj;
    float alpha=1.0f;
    private RectTransform rect;

    // Start is called before the first frame update
    void Start()
    {
        fadeObj = GameObject.Find("status");
        rect = fadeObj.GetComponent<RectTransform>();
    }
    

    void Update()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            FadeOut();
        }
    }

    void FadeOut()
    {
        fadeObj.GetComponent<Image>().color = new Color(1, 1, 1, alpha);
        alpha -= Time.deltaTime*3.0f;
        //rect.sizeDelta = new Vector2(0,0);
    }

    void FadeIn()
    {
        fadeObj.GetComponent<Image>().color = new Color(1, 1, 1, alpha);
        alpha += Time.deltaTime * 2;

    }
}
