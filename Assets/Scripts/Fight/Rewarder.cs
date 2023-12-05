using System;
using Assets.DefendItems;
using Assets.Infrastructure;
using Assets.Inventory;
using Assets.Inventory.ItemGeneratorSystem;
using Assets.UI.HUD;
using Assets.User;
using Assets.Weapons;

namespace Assets.Fight
{
    public class Rewarder
    {
        private bool _isBoosFight;
        private IPlayerPresenter _playerPresenter;
        private RewardPanel _rewardPanel;

        public Rewarder(bool isBoosFight, IPlayerPresenter playerPresenter, RewardPanel rewardPanel)
        {
            _isBoosFight = isBoosFight;
            _playerPresenter = playerPresenter;
            _rewardPanel = rewardPanel;
        }

        public void CreateBossReward(Action successCallback)
        {
            Armor randomArmor = ItemGenerator.Instance.GetRandomArmor(_isBoosFight);
            Weapon randomWeapon = ItemGenerator.Instance.GetRandomWeapon(_isBoosFight);
            int money = ItemGenerator.Instance.GetBossReward();

            Game.GameSettings.PlayerData.Money += money;
            _playerPresenter.Player.InventoryPresenter.InventoryModel.AddItem(randomWeapon);
            _playerPresenter.Player.InventoryPresenter.InventoryModel.AddItem(randomArmor);
            
            _rewardPanel.Show(randomArmor, randomWeapon, money);
            _rewardPanel.OnButtonClickEvent += successCallback;
        }

        public void CreateEnemyReward(Action successCallback)
        {
            IInventoryItem randomLoot = GetRandomLoot();
            int money = ItemGenerator.Instance.GetEnemyReward();
            
            Game.GameSettings.PlayerData.Money += money;
            _playerPresenter.Player.InventoryPresenter.InventoryModel.AddItem(randomLoot);
            
            _rewardPanel.Show(randomLoot, money);
            _rewardPanel.OnButtonClickEvent += successCallback;
        }

        private IInventoryItem GetRandomLoot()
        {
            if (UnityEngine.Random.Range(0, 2) == 0)
                return ItemGenerator.Instance.GetRandomArmor();
            
            return ItemGenerator.Instance.GetRandomWeapon();
        }
    }
}