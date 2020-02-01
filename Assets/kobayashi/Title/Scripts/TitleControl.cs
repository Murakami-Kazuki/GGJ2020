using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace Title
{
    public class TitleControl : MonoBehaviour
    {

        protected static TitleControl instance;
        public static TitleControl Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = (TitleControl)FindObjectOfType(typeof(TitleControl));
                    if (instance == null)
                    {
                        var name = typeof(TitleControl).Name;
                        Debug.LogFormat("Create singleton object: {0}", name);
                        GameObject obj = new GameObject(name);
                        instance = obj.AddComponent<TitleControl>();
                    }
                }
                return instance;
            }
        }

        public Action onNextSceneAction;
        [SerializeField] PressKeyText pressKeyText;
        bool isPressButton = false;

        void Start()
        {
            pressKeyText.Show();
        }

        void Update()
        {
            if (!isPressButton && IsPressButton()) OnPressAnyButton();
        }

        bool IsPressButton()
        {
            //TODO 入力受付
            if (Input.GetButtonDown("DS4square"))
            {
                return true;
            }
            if (Input.GetMouseButtonDown(0)) return true;
            return false;
        }

        void OnPressAnyButton()
        {
            isPressButton = true;
            //TODO scene移動
            FadeManager.Instance.Fade(new Color(1, 1, 1, 0), Color.white, 0.4f, false, TransNextScene);
        }


        void TransNextScene()
        {
            Debug.Log("ok. trans next scene.");

            onNextSceneAction();
        }
    }
}
