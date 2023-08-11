using System;

namespace Assets.Infrastructure.DataStorageSystem
{
    public class UserData
    {
        private int _money;
        private bool _isSoundOn;
        private bool _isSfxOn;
        
        public UserData()
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

        public bool IsSoundOn
        {
            get => _isSoundOn;
            set
            {
                if (value == _isSoundOn)
                    throw new AggregateException("Attempt to change the state to the same");
                
                _isSoundOn = value;
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

        public void SaveData()
        {
            var data = new Data(_money,
                Convert.ToInt32(_isSoundOn),
                Convert.ToInt32(_isSfxOn));
            
            DataLoader.SaveData(data);
        }

        private void LoadData()
        {
            var data = DataLoader.GetData();
            _money = data.Money;
            _isSoundOn = Convert.ToBoolean(data.Sound);
            _isSfxOn = Convert.ToBoolean(data.Sfx);
        }
    }
}