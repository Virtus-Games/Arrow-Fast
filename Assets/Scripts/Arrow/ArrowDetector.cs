using System.Collections;
using System.Collections.Generic;
using UnityEngine;
 

namespace Pontaap.Studio
{
    public class ArrowDetector : MonoBehaviour
    {

        private Animator m_anim;
        private ScreenHandler screenHandler;
           private void Start()
        {
             m_anim = GetComponent<Animator>();
            screenHandler = FindObjectOfType<ScreenHandler>();
           
        }
        #region  Variables
        public LayerMask arrowLayer;
        #endregion

        #region Main Methods


        private void OnCollisionEnter(Collision collision)
        {

            if (collision.gameObject.CompareTag("Arrow"))
            {
                  collision.transform.position = screenHandler.WorldPos;
                
                screenHandler.WorldPos = Vector3.zero;
                 m_anim.SetTrigger("ScaleTrigger");

                if ((GameManager.GetInstance.arrowCount == 0 || GameManager.GetInstance.circleCount == 0 ))
                    GameManager.GetInstance.SetGameState(GameState.Lose);
            }
        }

        #endregion



    }
}


