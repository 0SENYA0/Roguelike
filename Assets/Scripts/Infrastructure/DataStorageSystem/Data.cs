namespace Assets.Infrastructure.DataStorageSystem
{
    public struct Data
    {
        public readonly int Money;
        public readonly int Music;
        public readonly int Sfx;
        public readonly string Localization;
        public readonly string GameStatistics;
        public readonly int ArmorLevel;
        public readonly int WeaponLevel;
        public readonly int Potion;
        public readonly int Idol;

        public Data(int money, int music, int sfx, string localization, string gameStatistics, int armorLevel, int weaponLevel, int potion, int idol)
        {
            Money = money;
            Music = music;
            Sfx = sfx;
            Localization = localization;
            GameStatistics = gameStatistics;
            ArmorLevel = armorLevel;
            WeaponLevel = weaponLevel;
            Potion = potion;
            Idol = idol;
        }
    }
}