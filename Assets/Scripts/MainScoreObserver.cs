using System.Collections;
using System.Collections.Generic;
using UnityEngine;
 using TMPro;
using UnityEngine.SceneManagement;

namespace Pontaap.Studio
{
    public class MainScoreObserver : MonoBehaviour
    {
        public static TextMeshProUGUI mainScore;
        private static HolderControl holderControl;
         private void Start()
        {
            mainScore = GetComponent<TextMeshProUGUI>();
             holderControl = transform.parent.GetComponentInChildren<HolderControl>();
               if(SceneManager.GetActiveScene().buildIndex >1)
             mainScore.SetText((GameManager.GetInstance.currentScore).ToString());
          }

        /// <summary>
        /// Her hedefe vuruldu�u zaman skor g�ncellemesi.
        /// </summary>
        /// <param name="value">G�ncelleme miktar�.</param>
        public static void UpdateMainScore(int value)
        {
            int currentScore = int.Parse(mainScore.text);
            currentScore += value;
            GameManager.GetInstance.currentScore = currentScore; 
            mainScore.SetText(currentScore.ToString());
             holderControl.UpdateHolderPos(currentScore);
            //LevelManager.GetInstance.LoadNextScene();
        }

    }
}
