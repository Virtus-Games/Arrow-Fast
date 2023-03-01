using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Pontaap.Studio
{
    public class TargetManager : MonoBehaviour
    {
        public int targetCount;
        private BoxScoreManager boxScoreManager;
        public int totalMaxScore;

        private void Start()
        {
            targetCount = transform.childCount;
            boxScoreManager = transform.GetChild(0).GetComponent<BoxScoreManager>();
            totalMaxScore = targetCount * boxScoreManager.maxScore;
            HolderControl.MaxScoreInScene = totalMaxScore + GameManager.GetInstance.currentScore;
            GameManager.GetInstance.maxScore += totalMaxScore;
         }
    }
}
