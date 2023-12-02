using Assets.UI.SettingsWindow.Localization;

namespace Assets.Config
{
    public static class LanguageConfig
    {
        public const string RandomEventKey = "RandomEvent";
        public const string EnemyOrLootKey = "EnemyOrLoot";
        public const string WeaponKey = "Weapon";
        public const string DamageKey = "Damage";
        public const string ArmorKey = "Armor";
        public const string BodyKey = "Body";
        public const string HeadKey = "Head";
        public const string CountKey = "Amount";
        public const string DmgModifierKey = "DmgModifier";
        public const string SplashChanceKey = "SplashChance";
        public const string CriticalChanceKey = "CriticalChance";
        public const string MoneyKey = "Money";
        public const string BossKey = "Boss";

        public static string GetAnonymous(string lang)
        {
            switch (lang)
            {
                case Language.ENG:
                    return "Anonymous";
                case Language.RUS:
                    return "Аноним";
                case Language.TUR:
                    return "Anonim";
                default:
                    return "Anonymous";
            }
        }
    }
}