using UnityEngine;

namespace Assets.Infrastructure.DataStorageSystem
{
    public static class DataService
    {
        private const string MoneyKey = "money";
        private const string MusicKey = "music";
        private const string SfxKey = "sxf";
        private const string LocalizationKey = "localization";
        private const string GameStatisticsKey = "statistics";
        private const string ArmorKey = "armor";
        private const string WeaponKey = "weapon";
        private const string PotionKey = "potion";
        private const string IdolKey = "idol";

        private const int MoneyDefault = 0;
        private const int MusicDefault = 1;
        private const int SfxDefault = 1;
        private const string LocalizationDefault = "";
        private const string GameStatisticsDefault = "0;0;0";
        private const int ArmorLevelDefault = 1;
        private const int WeaponLevelDefault = 1;
        private const int PotionDefault = 0;
        private const int IdolDefault = 0;

        public static Data GetData()
        {
            int money = PlayerPrefs.HasKey(MoneyKey) ? PlayerPrefs.GetInt(MoneyKey) : MoneyDefault;
            int sound = PlayerPrefs.HasKey(MusicKey) ? PlayerPrefs.GetInt(MusicKey) : MusicDefault;
            int sfx = PlayerPrefs.HasKey(SfxKey) ? PlayerPrefs.GetInt(SfxKey) : SfxDefault;
            string lang = PlayerPrefs.HasKey(LocalizationKey) ? PlayerPrefs.GetString(LocalizationKey) : LocalizationDefault;
            string statistics = PlayerPrefs.HasKey(GameStatisticsKey) ? PlayerPrefs.GetString(GameStatisticsKey) : GameStatisticsDefault;
            int armorLevel = PlayerPrefs.HasKey(ArmorKey) ? PlayerPrefs.GetInt(ArmorKey) : ArmorLevelDefault;
            int weaponLevel = PlayerPrefs.HasKey(WeaponKey) ? PlayerPrefs.GetInt(WeaponKey) : WeaponLevelDefault;
            int potion = PlayerPrefs.HasKey(PotionKey) ? PlayerPrefs.GetInt(PotionKey) : PotionDefault;
            int idol = PlayerPrefs.HasKey(IdolKey) ? PlayerPrefs.GetInt(IdolKey) : IdolDefault;
            
            return new Data(money, sound, sfx, lang, statistics, armorLevel, weaponLevel, potion, idol);
        }

        public static void SaveData(Data data)
        {
            PlayerPrefs.SetInt(MoneyKey, data.Money);
            PlayerPrefs.SetInt(MusicKey, data.Music);
            PlayerPrefs.SetInt(SfxKey, data.Sfx);
            PlayerPrefs.SetString(LocalizationKey, data.Localization);
            PlayerPrefs.SetString(GameStatisticsKey, data.GameStatistics);
            PlayerPrefs.SetInt(ArmorKey, data.ArmorLevel);
            PlayerPrefs.SetInt(WeaponKey, data.WeaponLevel);
            PlayerPrefs.SetInt(PotionKey, data.Potion);
            PlayerPrefs.SetInt(IdolKey, data.Idol);
            PlayerPrefs.Save();
        }
    }
}