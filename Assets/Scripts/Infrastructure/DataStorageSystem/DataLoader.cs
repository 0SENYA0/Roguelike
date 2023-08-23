using UnityEngine;

namespace Assets.Infrastructure.DataStorageSystem
{
    public static class DataLoader
    {
        private const string MoneyKey = "money";
        private const string MusicKey = "music";
        private const string SfxKey = "sxf";

        private const int MoneyDefault = 0;
        private const int MusicDefault = 1;
        private const int SfxDefault = 1;

        public static Data GetData()
        {
            int money = PlayerPrefs.HasKey(MoneyKey) ? PlayerPrefs.GetInt(MoneyKey) : MoneyDefault;
            int sound = PlayerPrefs.HasKey(MusicKey) ? PlayerPrefs.GetInt(MusicKey) : MusicDefault;
            int sfx = PlayerPrefs.HasKey(SfxKey) ? PlayerPrefs.GetInt(SfxKey) : SfxDefault;
            
            return new Data(money, sound, sfx);
        }

        public static void SaveData(Data data)
        {
            PlayerPrefs.SetInt(MoneyKey, data.Money);
            PlayerPrefs.SetInt(MusicKey, data.Music);
            PlayerPrefs.SetInt(SfxKey, data.Sfx);
            PlayerPrefs.Save();
        }
    }

    public struct Data
    {
        public readonly int Money;
        public readonly int Music;
        public readonly int Sfx;

        public Data(int money, int music, int sfx)
        {
            Money = money;
            Music = music;
            Sfx = sfx;
        }
    }
}