using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleMobileAds.Api;
using TMPro;

namespace Pontaap.Studio
{

    public enum UpgradeType
    {
        arrowSpeed,
        arrowDuration,
        circleCount,
        arrowCount
    }
    public class AdsManager : Singleton<AdsManager>
    {

        public string bannerID = "";
        public string intersititialID = "";
        public string rewardedID = "";
        #region variables
        private BannerView bannerView;
        private InterstitialAd interstitialAd;
        private RewardedAd rewardedAd;


        #endregion


        #region properties
        private AdRequest GetRequest { get { return new AdRequest.Builder().Build(); } }
        #endregion

        private void Start()
        {
            MobileAds.Initialize((InitializationStatus init) =>
            { });
            RequestRewarded();
            RequestIntersititial();
        }
        #region Banner
        /// <summary>
        /// Bir banner reklam isteðinde bulunur.
        /// </summary>
        public void BannerRequest()
        {
            bannerView = new BannerView(bannerID, AdSize.Banner, AdPosition.Bottom);
            bannerView.LoadAd(GetRequest);
        }


        #endregion


        #region Intersititial

        /// <summary>
        /// Bir geçiþ reklamý istediðinde bulunur.
        /// </summary>
        public void RequestIntersititial()
        {

            if (interstitialAd != null)
            {
                interstitialAd.Destroy();
                interstitialAd = null;
            }

            InterstitialAd.Load(intersititialID, GetRequest, (InterstitialAd ad, LoadAdError err) =>
            {
                if (err != null || ad == null)
                {
                    return;
                }
                interstitialAd = ad;

                interstitialAd.OnAdFullScreenContentClosed += OnInterstitialClosed;
            });


        }

        public void ShowIntersitital()
        {
            if (interstitialAd != null && interstitialAd.CanShowAd())
            {
                interstitialAd.Show();

            }
        }

        private void OnInterstitialClosed()
        {

            RequestIntersititial();
            LevelManager.GetInstance.LoadNextScene();
        }



        #endregion


        #region Rewarded
        /// <summary>
        /// Bir ödüllü reklam istediðinde bulunur.
        /// </summary>
        public void RequestRewarded()
        {
            if (rewardedAd != null)
            {
                rewardedAd.Destroy();
                rewardedAd = null;
            }

            RewardedAd.Load(rewardedID, GetRequest, (RewardedAd ad, LoadAdError err) =>
              {
                  if (err != null || ad == null)
                  {
                      return;
                  }

                  rewardedAd = ad;


              });

        }

        /// <summary>
        /// Yüklenen ödüllü reklamý göstermek için kullanýlýr.
        /// </summary>
        public void ShowRewarded(UpgradeType upgradeType)
        {

            if (rewardedAd != null && rewardedAd.CanShowAd())
            {
                rewardedAd.Show((Reward rew) =>
                {
                    switch (upgradeType)
                    {
                        case UpgradeType.arrowSpeed:
                            GameManager.GetInstance.UpgradeArrowSpeedFromSO();
                            break;
                        case UpgradeType.arrowDuration:
                            GameManager.GetInstance.UpgradeArrowDisableDurationFromSO();
                            break;
                        case UpgradeType.circleCount:
                            GameManager.GetInstance.UpgradeCircleCountFromThrower();
                            break;
                        case UpgradeType.arrowCount:
                            GameManager.GetInstance.UpgradeArrowCountFromThrower();
                            break;
                    }
                });
            }
            RegisterEventHandler(rewardedAd);


        }


        private void RegisterEventHandler(RewardedAd ad)
        {

            ad.OnAdFullScreenContentClosed += () =>
            {

                RequestRewarded();
            };


            ad.OnAdFullScreenContentFailed += (AdError err) =>
            {
                RequestRewarded();
            };
        }
        #endregion
    }
}
