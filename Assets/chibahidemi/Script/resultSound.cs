using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class resultSound : MonoBehaviour
{
    private float ResultBgmStart = 5.0f;//BGMスタートタイミング
    private AudioSource sound01;
    // Start is called before the first frame update
    void Start()
    {
        AudioSource audioSource = GetComponent<AudioSource>();
        sound01 = audioSource;
        Invoke("result",ResultBgmStart);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void result()
    {
        sound01.PlayOneShot(sound01.clip);
    }
}
