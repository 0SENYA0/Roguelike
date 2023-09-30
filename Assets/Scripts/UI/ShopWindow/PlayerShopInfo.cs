using Assets.Infrastructure;
using Assets.ScriptableObjects;
using Assets.YandexAds;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.UI.ShopWindow
{
    public class PlayerShopInfo : MonoBehaviour
    {
        [SerializeField] private TMP_Text _information;
        [SerializeField] private PlayerShopInfoScriptableObject _localized;
        [SerializeField] private Button _addMoneyButton;
        [SerializeField] private int _additionalMoney = 1000;
        [SerializeField] private YandexAdView _yandexAd;

        private void OnEnable()
        {
            _addMoneyButton.onClick.AddListener(ShowRewardAd);
        }

        private void OnDisable()
        {
            _addMoneyButton.onClick.RemoveListener(ShowRewardAd);
        }

        public void UpdateInfo()
        {
            var lang = Game.GameSettings.CurrentLocalization;
            var stats = Game.GameSettings.PlayerData.GameStatistics;
            var money = Game.GameSettings.PlayerData.Money;
            
            var text = $"{_localized.NumberOfAttempts.GetLocalization(lang)}: {stats.NumberOfAttempts}\n\n" +
                       $"{_localized.NumberOfEnemy.GetLocalization(lang)}: {stats.NumberOfEnemiesKilled}\n\n" +
                       $"{_localized.NumberOfBoss.GetLocalization(lang)}: {stats.NumberOfBossesKilled}\n\n" +
                       $"{_localized.Money.GetLocalization(lang)}: {money}$";

            _information.text = text;
        }

        private void ShowRewardAd()
        {
            _yandexAd.ShowRewardVideo(AddMoneyToPlayer);
        }

        private void AddMoneyToPlayer()
        {
            Game.GameSettings.PlayerData.Money += _additionalMoney;
            UpdateInfo();
        }
    }
}