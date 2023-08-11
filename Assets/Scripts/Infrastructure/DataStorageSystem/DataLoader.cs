using UnityEngine;

namespace Assets.Infrastructure.DataStorageSystem
{
    public static class DataLoader
    {
        private const string MoneyKey = "money";
        private const string SoundKey = "sound";
        private const string SfxKey = "sxf";

        private const int MoneyDefault = 0;
        private const int SoundDefault = 1;
        private const int SfxDefault = 1;

        public static Data GetData()
        {
            int money = PlayerPrefs.HasKey(MoneyKey) ? PlayerPrefs.GetInt(MoneyKey) : MoneyDefault;
            int sound = PlayerPrefs.HasKey(SoundKey) ? PlayerPrefs.GetInt(SoundKey) : SoundDefault;
            int sfx = PlayerPrefs.HasKey(SfxKey) ? PlayerPrefs.GetInt(SfxKey) : SfxDefault;
            
            return new Data(money, sound, sfx);
        }

        public static void SaveData(Data data)
        {
            PlayerPrefs.SetInt(MoneyKey, data.Money);
            PlayerPrefs.SetInt(SoundKey, data.Sound);
            PlayerPrefs.SetInt(SfxKey, data.Sfx);
            PlayerPrefs.Save();
        }
    }

    public struct Data
    {
        public readonly int Money;
        public readonly int Sound;
        public readonly int Sfx;

        public Data(int money, int sound, int sfx)
        {
            Money = money;
            Sound = sound;
            Sfx = sfx;
        }
    }
}