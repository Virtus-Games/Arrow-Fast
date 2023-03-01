using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
 
namespace Pontaap.Studio
{

    public enum GameState
    {
        Play, Pause, Win, Lose,
    }
    public class GameManager : Singleton<GameManager>
    { 

        [HideInInspector]
        public int currentScore;

        [HideInInspector]
        public int maxScore;

        [HideInInspector]
        public int circleCount;

        [HideInInspector]
        public int arrowCount;

        [HideInInspector]
        public GameState gameState;

        [HideInInspector]
        public Material postProcessingMat; 

        public GameObject gameoverPanel;
        public GameObject SettingsPanel;
        public GameObject UpgradePanel;

        public TextMeshProUGUI circleCountText;
        public TextMeshProUGUI arrowCountText;
        public HolderControl holderControl;

        public ArrowSO arrowSO;
        private GameObject canvas;

        private CircleThrower[] circleThrowers;
        private ArrowThrower arrowThrower;

        public ParticleSystem scoreEffectPrefab;
        private ParticleSystem scoreEffectObj;


        private const float defaultArrowSpeed = 8f;
        private const float defaultDisableDuration = 2f;

      
        private void Start()
        {
            gameState = GameState.Play;
            SceneManager.activeSceneChanged += OnSceneChanged;
            Application.targetFrameRate = 60;
            canvas = transform.GetChild(0).gameObject;
            postProcessingMat = transform.GetChild(1).GetComponent<PostProcessing>().postEffectMat;
            postProcessingMat.SetFloat("_GrayScale", 0);
            scoreEffectObj = Instantiate(scoreEffectPrefab, transform, true);
            circleThrowers = FindObjectsOfType<CircleThrower>();
            arrowThrower = FindObjectOfType<ArrowThrower>();

            GetComponent<AudioSource>().volume = PlayerPrefs.GetFloat("VolumeFloat");
        }

        /// <summary>
        /// Sahne her deðiþtitðinde çalýþacak fonksiyon.
        /// </summary>
        /// <param name="current"></param>
        /// <param name="next"></param>
        void OnSceneChanged(Scene current, Scene next)
        {
            if(SceneManager.GetActiveScene().buildIndex !=8)
            {
                gameState = GameState.Play;
                canvas.SetActive(true);
                circleCount = 0;
                arrowCount = 0;
                holderControl.ResetHolder();
                circleThrowers = FindObjectsOfType<CircleThrower>();
                arrowThrower = FindObjectOfType<ArrowThrower>();
                arrowSO.throwForce = defaultArrowSpeed;
                arrowSO.disableDuration = defaultDisableDuration;
            }
            else
            {
                canvas.SetActive(false);

            }



        }

        /// <summary>
        /// Geçerli sahnedeki yüklenen circle sayýsýný ayarlar.
        /// </summary>
        /// <param name="value">Circle objelerinin azalma miktarý.</param>
        /// <param name="isInit">Eðer bu objeler baþlangýçta oluþturuluyorsa true olarak ayarlayýn.</param>
        public void UpdateCircleCount(int value = 1, bool isInit = false)
        {
            if (isInit)
                circleCount += value;
            else
                circleCount -= value;

            circleCountText.SetText(circleCount.ToString());

        }
        public void UpdateArrowCount(int value = 1, bool init = false)
        {
            if (init)
                arrowCount += value;
            else
                arrowCount--;
            arrowCountText.SetText(arrowCount.ToString());
        }




        public void PlayGame()
        {
            LevelManager.GetInstance.LoadNextScene();
        }

        public void QuitGame()
        {
            Application.Quit();
        }

        public void ScoreEffect(Vector3 pos)
        {
            scoreEffectObj.transform.position = pos;
            scoreEffectObj.Play();
        }
        public void SetSettingsPanel()
        {
            if (SettingsPanel.activeInHierarchy)
            {
                SettingsPanel.SetActive(false);
                gameState = GameState.Play;
            }

            else
            {
                if (UpgradePanel.activeInHierarchy)
                {
                    UpgradePanel.SetActive(false);
                }
                SettingsPanel.SetActive(true);
                gameState = GameState.Pause;
            }
        }
        public void SetUpgradesPanel()
        {
            if (UpgradePanel.activeInHierarchy)
            {
                UpgradePanel.SetActive(false);
                gameState = GameState.Play;
            }

            else
            {
                if (SettingsPanel.activeInHierarchy)
                    SettingsPanel.SetActive(false);

                UpgradePanel.SetActive(true);
                gameState = GameState.Pause;
            }
        }

        public void SetGameState(GameState gameState)
        {
            this.gameState = gameState;
            switch (gameState)
            {
                case GameState.Play:
                    break;
                case GameState.Win:
                    //LevelManager.GetInstance.LoadNextScene();
                    AdsManager.GetInstance.ShowIntersitital();

                    break;
                case GameState.Lose:
                    postProcessingMat.SetFloat("_GrayScale", 1);
                    gameoverPanel.SetActive(true);
                    LevelManager.GetInstance.GetComponent<AudioSource>().Play();
                    break;

            }
        }
        public void UpgradeArrowSpeedFromSO()
        {
            arrowSO.throwForce += 2;
        }   
        
        public void UpgradeArrowDisableDurationFromSO()
        {
            arrowSO.disableDuration += 1;
        }

        public void UpgradeArrowCountFromThrower()
        {
            arrowThrower.SpawnNewArrows(5);
        } 
        public void UpgradeCircleCountFromThrower()
        {
            foreach (CircleThrower item in circleThrowers)
            {
                item.SpawnNewCircles(5);
            }
        }

        #region Butonlar ile yapýlacak yükseltmeler
        public void UpgradeArrowSpeedFromAd()
        {
           AdsManager.GetInstance.ShowRewarded(UpgradeType.arrowSpeed);
             
        }
        public void UpgrdeArrowDeathDurationFromAd()
        {
             AdsManager.GetInstance.ShowRewarded(UpgradeType.arrowDuration);
        }
        public void UpgradeCircleCountFromAd()
        {
             AdsManager.GetInstance.ShowRewarded(UpgradeType.circleCount);
            
                
        }

        public void UpgradeArrowCountFromAd()
        {
             AdsManager.GetInstance.ShowRewarded(UpgradeType.arrowCount);
 
               

        }

        #endregion
        public void RestartLevel()
        {

            LevelManager.GetInstance.RestartThisScene();
            postProcessingMat.SetFloat("_GrayScale", 0);
            gameoverPanel.SetActive(false);
        }

    }
}
