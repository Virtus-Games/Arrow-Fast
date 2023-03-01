using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace Pontaap.Studio
{

    public enum IndicateType
    {
        Speed,
        DestroyDuration 
        
    }
    public class Indicator : MonoBehaviour
    {
        public RectTransform holder;
        public RectTransform fillBG;
        public TextMeshProUGUI amountText;
        public IndicateType indicateType;

        private ArrowSO currentArrow;
        private Coroutine coroutine;
        private Vector2 targetVectHolder;
        private Vector2 targetVectFillBG;
        private float width;

        private void Start()
        {
            currentArrow = GameManager.GetInstance.arrowSO;
            targetVectHolder = Vector2.zero;
            targetVectFillBG = new Vector2(0, fillBG.rect.height);
            width = GetComponent<RectTransform>().rect.width;

        }



        IEnumerator UpdateHolder()
        {
            while (true)
            {
                if (currentArrow)
                {

                    switch (indicateType)
                    {
                        case IndicateType.Speed:
                            targetVectHolder.x = currentArrow.throwForce * width / currentArrow.maxThrowForce;
                            targetVectFillBG.x = currentArrow.throwForce * width / currentArrow.maxThrowForce;

                            holder.anchoredPosition = Vector2.Lerp(holder.anchoredPosition, targetVectHolder, Time.deltaTime * 2);
                            fillBG.sizeDelta = Vector2.Lerp(fillBG.sizeDelta, targetVectFillBG, Time.deltaTime * 2);
                            amountText.SetText(((int)(currentArrow.throwForce)).ToString());

                            break;

                        case IndicateType.DestroyDuration:
                            targetVectHolder.x = currentArrow.disableDuration * width / currentArrow.maxDisableDuration;
                            targetVectFillBG.x = currentArrow.disableDuration * width / currentArrow.maxDisableDuration;

                            holder.anchoredPosition = Vector2.Lerp(holder.anchoredPosition, targetVectHolder, Time.deltaTime * 2);
                            fillBG.sizeDelta = Vector2.Lerp(fillBG.sizeDelta, targetVectFillBG, Time.deltaTime * 2);
                            amountText.SetText(((int)(currentArrow.disableDuration)).ToString());
                           
                            break;

                        
                    }



                }

                yield return null;
            }
        }

        private void OnEnable()
        {
            if (coroutine == null)
                coroutine = StartCoroutine(UpdateHolder());

            if (!currentArrow)
            {
                currentArrow = GameManager.GetInstance.arrowSO;

            }

 

        }

        private void OnDisable()
        {

            StopCoroutine(coroutine);
            coroutine = null;
            holder.anchoredPosition = Vector2.zero;
            fillBG.sizeDelta = new Vector2(0, fillBG.rect.height);

        }
    }
}
