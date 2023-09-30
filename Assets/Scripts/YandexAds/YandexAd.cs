using System;
using Agava.YandexGames;
using Assets.Infrastructure;
using UnityEngine;

namespace Assets.YandexAds
{
    public class YandexAd
    {
        public void ShowRewardVideo(Action rewardMethod)
        {
            if (Application.isEditor)
            {
                rewardMethod?.Invoke();
                return;
            }
            
            VideoAd.Show(
                onOpenCallback: Game.GameSettings.Sound.Pause,
                onRewardedCallback: rewardMethod.Invoke,
                onCloseCallback: Game.GameSettings.Sound.UpPause,
                onErrorCallback: (x) => Game.GameSettings.Sound.UpPause()
                );
        }

        public void ShowInterstitialAd(Action callback)
        {
            if (Application.isEditor)
            {
                callback?.Invoke();
                return;
            }
            
            InterstitialAd.Show(
                onOpenCallback: Game.GameSettings.Sound.Pause,
                onCloseCallback: (x) =>
                {
                    Game.GameSettings.Sound.UpPause();
                    callback?.Invoke();
                });
        }
    }
}