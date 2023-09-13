using Assets.UI.SettingsWindow.Localization;
using UnityEngine;

namespace Assets.Infrastructure.DataStorageSystem
{
    public static class DataManager
    {
        private const string MoneyKey = "money";
        private const string MusicKey = "music";
        private const string SfxKey = "sxf";
        private const string LocalizationKey = "localization";

        private const int MoneyDefault = 0;
        private const int MusicDefault = 1;
        private const int SfxDefault = 1;
        private const string LocalizationDefault = Language.ENG;

        public static Data GetData()
        {
            int money = PlayerPrefs.HasKey(MoneyKey) ? PlayerPrefs.GetInt(MoneyKey) : MoneyDefault;
            int sound = PlayerPrefs.HasKey(MusicKey) ? PlayerPrefs.GetInt(MusicKey) : MusicDefault;
            int sfx = PlayerPrefs.HasKey(SfxKey) ? PlayerPrefs.GetInt(SfxKey) : SfxDefault;
            string loc = PlayerPrefs.HasKey(LocalizationKey) ? PlayerPrefs.GetString(LocalizationKey) : LocalizationDefault;
            
            return new Data(money, sound, sfx, loc);
        }

        public static void SaveData(Data data)
        {
            PlayerPrefs.SetInt(MoneyKey, data.Money);
            PlayerPrefs.SetInt(MusicKey, data.Music);
            PlayerPrefs.SetInt(SfxKey, data.Sfx);
            PlayerPrefs.SetString(LocalizationKey, data.Localization);
            PlayerPrefs.Save();
        }
    }

    public readonly struct Data
    {
        public readonly int Money;
        public readonly int Music;
        public readonly int Sfx;
        public readonly string Localization;

        public Data(int money, int music, int sfx, string localization)
        {
            Money = money;
            Music = music;
            Sfx = sfx;
            Localization = localization;
        }
    }
}