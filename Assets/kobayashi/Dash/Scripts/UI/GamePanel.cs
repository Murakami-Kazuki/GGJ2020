using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Dash
{
    public class GamePanel : BasePanel
    {
        [SerializeField] Timer timer;
        public override void Activate()
        {
            base.Activate();
            timer.StartTimer();
        }
    }
}
