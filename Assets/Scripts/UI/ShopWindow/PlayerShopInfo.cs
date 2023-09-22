using Assets.Infrastructure;
using Assets.ScriptableObjects;
using TMPro;
using UnityEngine;

namespace Assets.UI.ShopWindow
{
    public class PlayerShopInfo : MonoBehaviour
    {
        [SerializeField] private TMP_Text _information;
        [SerializeField] private PlayerShopInfoScriptableObject _localized;

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
    }
}