using System;
using Agava.WebUtility;
using Assets.Infrastructure;
using UnityEngine;

namespace Assets.YandexAds
{
    public class YandexAdView : MonoBehaviour
    {
        private YandexAd _yandexAd;

        private void Start()
        {
            _yandexAd = new YandexAd();
        }

        private void OnEnable()
        {
            WebApplication.InBackgroundChangeEvent += WebApplicationFocus;
        }

        private void OnDisable()
        {
            WebApplication.InBackgroundChangeEvent -= WebApplicationFocus;
        }

        public void ShowRewardVideo(Action rewardMethod)
        {
            _yandexAd.ShowRewardVideo(rewardMethod);
        }

        public void ShowInterstitialAd(Action callback)
        {
            _yandexAd.ShowInterstitialAd(callback);
        }

        private void WebApplicationFocus(bool unfocusStatus)
        {
            // if (unfocusStatus)
            //     Time.timeScale = 0;
            // else
            //     Time.timeScale = 1;
        }

        private void OnApplicationFocus(bool pauseStatus)
        {
            // if (pauseStatus == false)
            //     Time.timeScale = 0;
            // else
            //     Time.timeScale = 1;
        }
    }
}