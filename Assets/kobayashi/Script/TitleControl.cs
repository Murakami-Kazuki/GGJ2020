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
        void Update()
        {
            if (IsPressButton()) OnTapAnyButton();
        }

        bool IsPressButton()
        {
            //TODO 入力受付
            if (Input.GetMouseButton(0)) return true;
            return false;
        }

        void OnTapAnyButton()
        {
            //TODO scene移動
            TransNextScene();
        }


        void TransNextScene()
        {
            Debug.Log("ok. trans next scene.");

            onNextSceneAction();
        }
    }
}
