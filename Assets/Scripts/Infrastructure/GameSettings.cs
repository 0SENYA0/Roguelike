using System;
using Assets.Infrastructure.DataStorageSystem;
using Assets.Scripts.SoundSystem;
using Assets.UI.ShopWindow;
using UnityEngine;

namespace Assets.Infrastructure
{
    public class GameSettings: IDisposable
    {
        private Sound _sound;
        private PlayerData _playerData;

        public event Action<string> OnLanguageChange;

        public GameSettings()
        {
            Initialization();
        }

        public ISound Sound => _sound;
        public PlayerData PlayerData => _playerData;
        public string CurrentLocalization => _playerData.Localization;

        public void Dispose()
        {
            _sound.Dispose();
            _playerData.SaveData();
        }

        private void Initialization()
        {
            _playerData = new PlayerData();
            _sound = new Sound(_playerData);
            Debug.Log(_playerData);
        }

        public void ChangeSoundSettings(SoundType type)
        {
            if (type == SoundType.Music)
                _playerData.IsMusicOn = !_playerData.IsMusicOn;
            else
                _playerData.IsSfxOn = !_playerData.IsSfxOn;
            
            _playerData.SaveData();
            _sound.UpdateSoundSettings(type);
        }

        public void ChangeLocalization(string lang)
        {
            _playerData.Localization = lang;
            _playerData.SaveData();
            OnLanguageChange?.Invoke(lang);
        }

        public bool TryBueItem(ShopItemType type, int price)
        {
            bool isBue = false;
            
            switch (type)
            {
                case ShopItemType.Armor:
                    if (CheckForPossibilityOfBuying(price))
                    {
                        _playerData.Money -= price;
                        _playerData.ArmorLevel++;
                        isBue = true;
                    }
                    break;
                case ShopItemType.Weapon:
                    if (CheckForPossibilityOfBuying(price))
                    {
                        _playerData.Money -= price;
                        _playerData.WeaponLevel++;
                        isBue = true;
                    }
                    break;
                case ShopItemType.Potion:
                    if (CheckForPossibilityOfBuying(price))
                    {
                        _playerData.Money -= price;
                        _playerData.Potion++;
                        isBue = true;
                    }
                    break;
                case ShopItemType.Idol:
                    if (CheckForPossibilityOfBuying(price))
                    {
                        _playerData.Money -= price;
                        _playerData.Idol++;
                        isBue = true;
                    }
                    break;
            }
            
            PlayerData.SaveData();
            
            return isBue;
        }

        private bool CheckForPossibilityOfBuying(int payment) => _playerData.Money - payment >= 0;
    }
}