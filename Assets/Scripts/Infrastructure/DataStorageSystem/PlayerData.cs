using System;
using Agava.YandexGames;
using Assets.UI.SettingsWindow.Localization;

namespace Assets.Infrastructure.DataStorageSystem
{
    public class PlayerData: IPlayerData
    {
        private int _money;
        private bool _isMusicOn;
        private bool _isSfxOn;
        private string _localization;
        private string _gameStatistics;
        private int _armorLevel;
        private int _weaponLevel;
        private int _potion;
        private int _idol;
        
        public PlayerData()
        {
            LoadData();
        }

        public int Money
        {
            get => _money;
            set
            {
                if (value < 0)
                    throw new ArgumentException("Money can't be negative");

                _money = value;
                SaveData();
            }
        }

        public bool IsMusicOn
        {
            get => _isMusicOn;
            set
            {
                if (value == _isMusicOn)
                    throw new AggregateException("Attempt to change the state to the same");
                
                _isMusicOn = value;
                SaveData();
            }
        }

        public bool IsSfxOn
        {
            get => _isSfxOn;
            set
            {
                if (value == _isSfxOn)
                    throw new AggregateException("Attempt to change the state to the same");

                _isSfxOn = value;
                SaveData();
            }
        }

        public string Localization
        {
            get => _localization;
            set => _localization = value;
        }

        public GameStatistics GameStatistics
        {
            get => new GameStatistics(_gameStatistics);
            set
            {
                if (CheckGameStatistics(value))
                    _gameStatistics = value.ConvertValueToStringLine();
                else
                    throw new AggregateException("Statistics values can only increase");
                SaveData();
            }
        }

        public int ArmorLevel
        {
            get => _armorLevel;
            set
            {
                if (CheckValueForDifferenceInOne(_armorLevel, value) == false)
                    throw new ArgumentException("The argument must differ from the old value by one and only increase the value");
                
                if (CheckForAnIncreaseInValue(_armorLevel, value) == false)
                    throw new ArgumentException("The argument should only increase the value");

                _armorLevel = value;
                SaveData();
            }
        }
        
        public int WeaponLevel
        {
            get => _weaponLevel;
            set
            {
                if (CheckValueForDifferenceInOne(_weaponLevel, value) == false)
                    throw new ArgumentException("The argument must differ from the old value by one and only increase the value");
                
                if (CheckForAnIncreaseInValue(_weaponLevel, value) == false)
                    throw new ArgumentException("The argument should only increase the value");

                _weaponLevel = value;
                SaveData();
            }
        }
        
        public int Potion
        {
            get => _potion;
            set
            {
                if (CheckValueForDifferenceInOne(_potion, value))
                    _potion = value;
                else
                    throw new ArgumentException("The argument must differ from the old value by one and only increase the value");
                
                SaveData();
            }
        }
        
        public int Idol
        {
            get => _idol;
            set
            {
                if (CheckValueForDifferenceInOne(_idol, value))
                    _idol = value;
                else
                    throw new ArgumentException("The argument must differ from the old value by one");

                SaveData();
            }
        }

        public override string ToString()
        {
            var message = $"money: {_money} | music: {_isMusicOn} | sfx: {_isSfxOn} |" +
                          $"lang: {_localization} | stats: {_gameStatistics} | armor: {_armorLevel} |" +
                          $"weapon: {_weaponLevel} | potion: {_potion} | idol: {_idol}";
            return message;
        }

        public void SaveData()
        {
            var data = new Data(
                _money,
                Convert.ToInt32(_isMusicOn),
                Convert.ToInt32(_isSfxOn),
                _localization,
                _gameStatistics,
                _armorLevel,
                _weaponLevel,
                _potion,
                _idol);
            
            DataManager.SaveData(data);
        }

        public void ResetData()
        {
            var data = DataManager.GetDefaultData();
            
            _money = data.Money;
            _money = 100000;
            _isMusicOn = Convert.ToBoolean(data.Music);
            _isSfxOn = Convert.ToBoolean(data.Sfx);
            _localization = data.Localization;
            _gameStatistics = data.GameStatistics;
            _armorLevel = data.ArmorLevel;
            _weaponLevel = data.WeaponLevel;
            _potion = data.Potion;
            _idol = data.Idol;
        }

        private void LoadData()
        {
            var data = DataManager.GetData();
            
            _money = data.Money;
            _money = 100000;
            _isMusicOn = Convert.ToBoolean(data.Music);
            _isSfxOn = Convert.ToBoolean(data.Sfx);
            _localization = CheckLocalization(data.Localization);
            _gameStatistics = data.GameStatistics;
            _armorLevel = data.ArmorLevel;
            _weaponLevel = data.WeaponLevel;
            _potion = data.Potion;
            _idol = data.Idol;
        }

        private string CheckLocalization(string lang)
        {
            if (lang == "")
            {
                lang = Language.DefineLanguage(YandexGamesSdk.Environment.i18n.lang);
            }

            return lang;
        }

        private bool CheckGameStatistics(GameStatistics newStat)
        {
            var currentStat = new GameStatistics(_gameStatistics);
            var isCurrentAttempts = newStat.NumberOfAttempts - currentStat.NumberOfAttempts >= 0;
            var isCurrentEnemies = newStat.NumberOfEnemiesKilled - currentStat.NumberOfEnemiesKilled >= 0;
            var isCurrentBosses = newStat.NumberOfBossesKilled - currentStat.NumberOfBossesKilled >= 0;

            return isCurrentAttempts && isCurrentEnemies && isCurrentBosses;
        }

        private bool CheckValueForDifferenceInOne(int oldValue, int newValue)
        {
            return MathF.Abs(oldValue - newValue) == 1 && newValue >= 0;
        }

        private bool CheckForAnIncreaseInValue(int oldValue, int newValue)
        {
            return newValue - oldValue > 0;
        }
    }
}