using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Title
{
    [RequireComponent(typeof(AudioSource))]
    public class TitleAudio : MonoBehaviour
    {
        [SerializeField] AudioClip bgm;
        [SerializeField] AudioClip buttonPress;

        AudioSource myAudio;

        void Awake()
        {
            myAudio = GetComponent<AudioSource>();
            TitleControl.Instance.onNextSceneAction += NextSceneActionCallback;
        }

        void StartBGM()
        {
            myAudio.clip = bgm;
            myAudio.Play();
        }

        void NextSceneActionCallback()
        {
            StopBGM();
        }

        void StopBGM()
        {
            myAudio.Stop();
            Debug.Log("BGM stop");
        }
    }
}