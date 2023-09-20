using System;
using Assets.Enemy;
using Assets.Inventory;
using Assets.Inventory.ItemGeneratorSystem;
using Assets.Player;
using Assets.Scripts.InteractiveObjectSystem;
using Assets.Scripts.SoundSystem;
using Assets.UI;
using Assets.UI.HUD;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.Fight
{
    public class UIFight : MonoBehaviour
    {
        [SerializeField] private GameObject _globalMap;
        [SerializeField] private GameObject _battlefieldMap;
        [SerializeField] private FightPlace _fightPlace;
        [SerializeField] private InteractiveObjectHandler _interactiveObjectHandler;
        [SerializeField] private RewardPanel _rewardPanel;
        [SerializeField] private LosePanel _losePanel;
        [SerializeField] private SoundComponent _fightSound;

        private IPlayerPresenter _playerPresenter;

        public void SetActiveFightPlace(IPlayerPresenter playerPresenter, IEnemyPresenter enemyPresenter)
        {
            _playerPresenter = playerPresenter;
            _battlefieldMap.SetActive(true);
            _fightSound.Play();
            _globalMap.SetActive(false);
            
            _fightPlace.FightEnded += ShowRewardPanel;
            _fightPlace.Set(playerPresenter, enemyPresenter);
        }

        private void ShowRewardPanel(FightResult fightResult)
        {
            _fightPlace.FightEnded -= ShowRewardPanel;

            switch (fightResult)
            {
                case FightResult.Win:
                    var randomLoot = GetRandomLoot();
                    _rewardPanel.Show(randomLoot);
                    _rewardPanel.OnButtonClickEvent += ShowGlobalMap;
                    _playerPresenter.Player.InventoryPresenter.InventoryModel.AddItem(randomLoot);
                    break;
                case FightResult.Lose:
                    _losePanel.Show("Вы проиграли (((");
                    _losePanel.OnButtonClickEvent += OnLosePanelClick;
                    break;
                case FightResult.Leave:
                    ShowGlobalMap();
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(fightResult), fightResult, null);
            }
        }

        private void OnLosePanelClick()
        {
            _fightSound.Stop();
            _losePanel.OnButtonClickEvent -= OnLosePanelClick;
            
            Curtain.Instance.ShowAnimation(() =>
            {
                SceneManager.LoadScene("Menu");
            });
        }

        private void ShowGlobalMap()
        {
            _fightSound.Stop();
            _rewardPanel.OnButtonClickEvent -= ShowGlobalMap;
            _rewardPanel.Hide();
            
            Curtain.Instance.ShowAnimation(() =>
            {
                _globalMap.SetActive(true);
                _battlefieldMap.SetActive(false);
                _interactiveObjectHandler.ReturnToGlobalMap();
            });
        }

        private IInventoryItem GetRandomLoot()
        {
            if (UnityEngine.Random.Range(0, 2) == 0)
                return ItemGenerator.Instance.GetRandomArmor();
            
            return ItemGenerator.Instance.GetRandomWeapon();
        }
    }
}