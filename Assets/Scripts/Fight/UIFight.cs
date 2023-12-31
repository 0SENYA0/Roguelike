using System;
using System.Linq;
using Assets.DefendItems;
using Assets.Enemy;
using Assets.Infrastructure;
using Assets.Inventory;
using Assets.Inventory.ItemGeneratorSystem;
using Assets.Scripts.InteractiveObjectSystem;
using Assets.Scripts.SoundSystem;
using Assets.UI;
using Assets.UI.HUD;
using Assets.UI.HUD.LosePanels;
using Assets.User;
using Assets.Weapons;
using UnityEngine;

namespace Assets.Fight
{
    public class UIFight : MonoBehaviour
    {
        [SerializeField] private LevelRoot _levelRoot;
        [SerializeField] private GameObject _globalMap;
        [SerializeField] private GameObject _battlefieldMap;
        [SerializeField] private FightPlace _fightPlace;
        [SerializeField] private InteractiveObjectHandler _interactiveObjectHandler;
        [SerializeField] private RewardPanel _rewardPanel;
        [SerializeField] private LosePanel _losePanel;
        [SerializeField] private SoundService _fightSound;

        private IPlayerPresenter _playerPresenter;
        private int _countOfEnemyForFight;
        private bool _isBoosFight;
        private Rewarder _rewarder;

        public void SetActiveFightPlace(IPlayerPresenter playerPresenter, IEnemyPresenter enemyPresenter)
        {
            _playerPresenter = playerPresenter;
            _battlefieldMap.SetActive(true);
            _fightSound.Play();
            _globalMap.SetActive(false);

            _countOfEnemyForFight = enemyPresenter.Enemy.Count;
            var enemyType = enemyPresenter.Enemy.FirstOrDefault(x => x.IsBoss);
            _isBoosFight = enemyType?.IsBoss ?? false;
            _rewarder = new Rewarder(_isBoosFight, _playerPresenter, _rewardPanel);

            _fightPlace.FightEnded += ShowRewardPanel;
            _fightPlace.Set(playerPresenter, enemyPresenter);
        }

        private void ShowRewardPanel(FightResult fightResult)
        {
            _fightPlace.FightEnded -= ShowRewardPanel;
            _levelRoot.IncreaseKilledEnemies(_countOfEnemyForFight);

            switch (fightResult)
            {
                case FightResult.Win:
                    if (_isBoosFight)
                        _rewarder.CreateBossReward(LoadNextLevel);
                    else
                        _rewarder.CreateEnemyReward(ShowGlobalMap);
                    break;
                case FightResult.Lose:
                    _losePanel.Show(_isBoosFight);
                    _losePanel.UserAnswerEvent += OnLosePanelClick;
                    break;
                case FightResult.Leave:
                    ShowGlobalMap();
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(fightResult), fightResult, null);
            }
        }

        private void OnLosePanelClick(UserLossAnswers answers)
        {
            _fightSound.Stop();
            _losePanel.UserAnswerEvent -= OnLosePanelClick;
            _losePanel.Hide();
            Reborn reborn = new Reborn(_playerPresenter, _globalMap, _battlefieldMap, _interactiveObjectHandler);

            switch (answers)
            {
                case UserLossAnswers.Ad:
                    _levelRoot.RebornWithAd(reborn.RebornWithAD);
                    break;
                case UserLossAnswers.Idol:
                    reborn.RebornWithIdol();
                    break;
                case UserLossAnswers.Menu:
                    _levelRoot.LoadMainMenu();
                    break;
            }
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

        private void LoadNextLevel()
        {
            _fightSound.Stop();
            _rewardPanel.OnButtonClickEvent -= LoadNextLevel;
            _levelRoot.LoadNextLevel();
        }
    }
}