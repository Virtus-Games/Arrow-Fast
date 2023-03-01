using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
namespace Pontaap.Studio
{
    public class BoxScoreManager : MonoBehaviour
    {
        public TextMeshProUGUI scoreText;
        public int incScore;
        public int maxScore;
        private int newScore = 0;
        private Material material;
        private bool completed;

        [HideInInspector]
        public float matColorChannel;
        private TargetManager targetManager;
         private void Start()
        {
            Init();
        }
        void Init()
        {

            scoreText.SetText(newScore.ToString());
            material = GetComponent<MeshRenderer>().sharedMaterial;
            material.SetFloat("_RedChannel", 0);
            material.SetFloat("_GreenChannel", 1);
            material.SetFloat("_AlphaChannel", 1);
            targetManager = transform.parent.GetComponent<TargetManager>();
         }

        /// <summary>
        /// Hedefe her vurulduðu zaman kendi içindeki score güncellemesi.
        /// </summary>
        public void UpdateScore()
        {
            newScore += incScore;

            if (newScore <= maxScore)
            {
                scoreText.SetText(newScore.ToString());
                matColorChannel = Mathf.InverseLerp(matColorChannel, maxScore, newScore);
                material.SetFloat("_RedChannel", matColorChannel);
                material.SetFloat("_GreenChannel", 1 - matColorChannel);
                material.SetFloat("_AlphaChannel", 1 - matColorChannel / 2);
                MainScoreObserver.UpdateMainScore(incScore);
            }
            else
            {
                if (!completed)
                { 
                    targetManager.targetCount--;
                    
                }


            }


        }

    }
}
