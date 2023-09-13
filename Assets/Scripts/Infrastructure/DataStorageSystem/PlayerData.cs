using System;

namespace Assets.Infrastructure.DataStorageSystem
{
    public class PlayerData: IPlayerData
    {
        private int _money;
        private bool _isMusicOn;
        private bool _isSfxOn;
        private string _localization;
        
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
            }
        }

        public string Localization
        {
            get => _localization;
            set => _localization = value;
        }

        public void SaveData()
        {
            var data = new Data(
                _money,
                Convert.ToInt32(_isMusicOn),
                Convert.ToInt32(_isSfxOn),
                _localization);
            
            DataManager.SaveData(data);
        }

        private void LoadData()
        {
            var data = DataManager.GetData();
            _money = data.Money;
            _isMusicOn = Convert.ToBoolean(data.Music);
            _isSfxOn = Convert.ToBoolean(data.Sfx);
            _localization = data.Localization;
        }
    }
}