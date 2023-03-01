using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Pontaap.Studio
{
    public class HolderControl : MonoBehaviour
    {
        public RectTransform holderRect;
        private RectTransform rect;
        public Vector2 delta;
        public float width;
        public float xMax;
        public float xPos;
         public static float MaxScoreInScene { get; set; }
        private void Awake()
        {
            rect = GetComponent<RectTransform>();
            width = rect.rect.width;
            xMax = rect.rect.xMax;
            delta = new Vector2(rect.rect.xMin, 0);
             StartCoroutine(UpdateHolderPos());
        }

        public void ResetHolder()
        {
            delta = new Vector2(rect.rect.xMin, 0);
        }
        public void UpdateHolderPos(int value)
        {
            xPos = Mathf.InverseLerp(0, MaxScoreInScene, value);
            delta.x = width * xPos - xMax;

        }

        IEnumerator UpdateHolderPos()
        {
             while (true)
            {

                holderRect.anchoredPosition = Vector2.Lerp(holderRect.anchoredPosition, delta, Time.deltaTime);
                 
               
                if (holderRect.anchoredPosition.x > xMax -50 && GameManager.GetInstance.gameState == GameState.Play)
                {
                     
                     GameManager.GetInstance.SetGameState(GameState.Win);

                }
                 yield return null;

            }

        }






    }
}
