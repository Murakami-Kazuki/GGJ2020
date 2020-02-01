﻿using System.Collections;
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
        [SerializeField] Fade fade;
        bool isPressButton = false;
        void Update()
        {
            if (!isPressButton && IsPressButton()) OnPressAnyButton();
        }

        bool IsPressButton()
        {
            //TODO 入力受付
            if (Input.GetMouseButtonDown(0)) return true;
            return false;
        }

        void OnPressAnyButton()
        {
            isPressButton = true;
            //TODO scene移動
            fade.FadeOut(0.4f, TransNextScene);
        }


        void TransNextScene()
        {
            Debug.Log("ok. trans next scene.");

            onNextSceneAction();
        }
    }
}