using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Pontaap.Studio
{
    public class UIAnimation : MonoBehaviour
    {

        private RectTransform targetImageRect;
        private RectTransform targetTextRect;
        private Vector2 targetSizeDelta;
        private Vector3 targetScale;
        private void Start()
        {
            StartCoroutine(UIAnimator());
        }

        public void HandleSizeDeltaAnim(float SizeDelta)
        {
            targetImageRect = EventSystem.current.currentSelectedGameObject.GetComponent<RectTransform>();
            targetSizeDelta = new Vector2(SizeDelta,SizeDelta);
        }
        public void HandleScaleAnim(float scale)
        {
            targetTextRect = EventSystem.current.currentSelectedGameObject.GetComponent<RectTransform>();
            targetScale = new Vector3(scale, scale, 1);
        }

      

        IEnumerator UIAnimator()
        {
            while (true)
            {

                if(targetImageRect)
                {
                    targetImageRect.sizeDelta = Vector2.Lerp(targetImageRect.sizeDelta, targetSizeDelta, Time.deltaTime * 20);
                    if(Mathf.Abs(targetSizeDelta.x - targetImageRect.sizeDelta.x) <2)
                    {
                        targetImageRect = null;
                    }
                }

                if(targetTextRect)
                {
                    targetTextRect.localScale = Vector3.Lerp(targetTextRect.localScale, targetScale, Time.deltaTime*20);
                    if(Mathf.Abs(targetTextRect.localScale.x -targetScale.x) <.2f)
                    {
                        targetTextRect = null;
                    }
                }

                if(Input.GetKey(KeyCode.Space))
                {
                    Time.timeScale = 0.2f;
                    Time.fixedDeltaTime = Time.timeScale * 0.02f;
                }

                if (Input.GetKeyUp(KeyCode.Space))
                {
                    Time.timeScale = 1;
                    Time.fixedDeltaTime = Time.timeScale * 0.02f;
                }

                yield return null;
            }
        }
    }
}
