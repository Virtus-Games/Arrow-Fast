using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Pontaap.Studio
{
    public class InputManager : InputBase
    {
        public bool onMobile;
         
        protected override void HandleInput()
        {
            if(!onMobile)
            base.HandleInput();
        }
    }
}
