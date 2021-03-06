﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerVoice : SingletonMonoBehaviour<playerVoice>
{
    AudioSource audioSource;
    [SerializeField] AudioClip screem1, screem2, screem3, charge1, charge2, charge3, damage1, damage2, damage3;
    bool cooltime, cooltime2, cooltime3;
    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(cooltime + "" + cooltime2);
    }

    public IEnumerator ScreemVC()
    {
        StopCoroutine(ChargeVC());
        if (cooltime == false)
        {
            cooltime = true;

            int number;
            number = Random.Range(1, 3);

            if (number == 1) audioSource.clip = screem1;
            if (number == 2) audioSource.clip = screem1;
            if (number == 3) audioSource.clip = screem3;
            audioSource.Play();
        }
        StopCoroutine(ChargeVC());
        yield return new WaitForSeconds(1);


        cooltime = false;


    }
    public IEnumerator ChargeVC()
    {


        if (cooltime2 == false)
        {
            cooltime2 = true;
            if (audioSource.clip != charge1 && audioSource.clip != charge2 && audioSource.clip != charge3)
            {

                int number;
                number = Random.Range(1, 3);

                if (number == 1) audioSource.clip = charge1;
                if (number == 2) audioSource.clip = charge2;
                if (number == 3) audioSource.clip = charge3;
                audioSource.Play();

            }




        }

        yield return new WaitForSeconds(0.5f);


        cooltime2 = false;
    }
    public IEnumerator DamagteVC()
    {
        if (cooltime3 == false)
        {
            Debug.Log("startDamgeVC2");
            cooltime3 = true;
            int number;
            number = Random.Range(1, 3);
            if (number == 1) audioSource.PlayOneShot(damage1);
            if (number == 2) audioSource.PlayOneShot(damage2);
            if (number == 3) audioSource.PlayOneShot(damage3);

        }
        yield return new WaitForSeconds(0.25f);

        cooltime3 = false;
    }


}
