using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Dash
{
    public class BasePanel : MonoBehaviour
    {
        [SerializeField] PanelType type = PanelType.Start;
        public PanelType MyType
        {
            get { return type; }
        }
        public bool IsActive { get; protected set; }

        public virtual void Activate()
        {
            IsActive = true;
            gameObject.SetActive(true);
        }

        public virtual void Deactivate()
        {
            IsActive = false;
            gameObject.SetActive(false);
        }

        public enum PanelType
        {
            Start,
            Game,
            Finish,
        }
    }
}