using Assets.Enemy;
using Assets.Inventory;
using Assets.Inventory.ItemGeneratorSystem;
using Assets.Player;
using Assets.Scripts.InteractiveObjectSystem;
using Assets.UI;
using Assets.UI.HUD;
using DefaultNamespace.Tools;
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

        private IPlayerPresenter _playerPresenter;

        public void SetActiveFightPlace(IPlayerPresenter playerPresenter, IEnemyPresenter enemyPresenter)
        {
            
            _playerPresenter = playerPresenter;
            _battlefieldMap.SetActive(true);
            _globalMap.SetActive(false);
            
            _fightPlace.FightEnded += ShowRewardPanel;
            _fightPlace.Set(playerPresenter, enemyPresenter);
        }

        private void ShowRewardPanel()
        {
            _fightPlace.FightEnded -= ShowRewardPanel;
            ConsoleTools.LogSuccess($"{_playerPresenter.Player.Healh}");
            
            if (_playerPresenter.Player.Healh <= 0)
            {
                _losePanel.Show("Вы проиграли (((");
                _losePanel.OnButtonClickEvent += OnLosePanelClick;
                
            }
            else
            {
                var randomLoot = GetRandomLoot();
                _rewardPanel.Show(randomLoot);
                _rewardPanel.OnButtonClickEvent += ShowGlobalMap;
                _playerPresenter.Player.InventoryPresenter.InventoryModel.AddItem(randomLoot);
            }
        }

        private void OnLosePanelClick()
        {
            Curtain.Instance.ShowAnimation(() =>
            {
                SceneManager.LoadScene("Menu");
            });
        }

        private IInventoryItem GetRandomLoot()
        {
            if (UnityEngine.Random.Range(0, 2) == 0)
                return ItemGenerator.Instance.GetRandomArmor();
            
            return ItemGenerator.Instance.GetRandomWeapon();
        }

        private void ShowGlobalMap()
        {
            _rewardPanel.OnButtonClickEvent -= ShowGlobalMap;
            _rewardPanel.Hide();
            
            Curtain.Instance.ShowAnimation(() =>
            {
                _globalMap.SetActive(true);
                _battlefieldMap.SetActive(false);
                _interactiveObjectHandler.ReturnToGlobalMap();
            });
        }
    }
}