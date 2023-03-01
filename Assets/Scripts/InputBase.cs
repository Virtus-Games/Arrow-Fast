using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Pontaap.Studio
{
    public class InputBase : MonoBehaviour
    {
        #region variables
         #endregion

        private void Update()
        {
            HandleInput();
        }

        protected virtual void HandleInput()
        {
            if (Input.GetMouseButtonDown(0))
                EventManager.AFire();
              
        }
    }

}
