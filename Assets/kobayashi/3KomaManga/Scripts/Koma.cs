using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class Koma : BaseKoma
{
    [SerializeField] AnimationCurve movementCurve;
    [SerializeField] bool hasDuration;

    private AudioSource Den01;
    private AudioSource Den02;
    private AudioSource Foo;
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

    void Start()
    {
        AudioSource[] audioSources = GetComponents<AudioSource>();
        Den01 = audioSources[0];
        Den02 = audioSources[1];
        Foo = audioSources[2];
        Invoke("Den01SE",1.2f);
        Invoke("Den02SE", 2.2f);
        Invoke("FooSE", 3.2f);

    }

    void Den01SE()
    {
        Den01.PlayOneShot(Den01.clip);
     }

    void Den02SE()
    {
        Den02.PlayOneShot(Den02.clip);
    }
    void FooSE()
    {
        Foo.PlayOneShot(Foo.clip);
    }
}
