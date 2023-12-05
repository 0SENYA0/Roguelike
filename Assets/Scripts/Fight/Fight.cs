using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Assets.Enemy;
using Assets.Fight.Dice;
using Assets.Interface;
using Assets.Person;
using Assets.Scripts.UI.Widgets;
using Assets.User;
using Assets.Utils;
using UnityEngine;
using AnimationState = Assets.Scripts.AnimationComponent.AnimationState;

namespace Assets.Fight
{
	public class Fight : IDisposable
	{
		private readonly ICoroutineRunner _coroutineRunner;
		private readonly List<EnemyAttackPresenter> _enemyAttackPresenters;
		private readonly PlayerAttackPresenter _playerAttackPresenter;
		private readonly IStepFightView _stepFightView;
		private readonly DicePresenterAdapter _dicePresenterAdapter;
		private readonly PlayerWeaponPanel _elementsDamagePanel;
		private readonly GameObject _popupReady;
		private readonly CustomButton _customButtonReady;
		private readonly Queue<UnitAttackPresenter> _unitsOfQueue;
		private readonly int _countSteps = 10;
		private readonly GeneratorAttackSteps _generatorAttackSteps;

		private Coroutine _coroutine;
		private bool _userIsReady;
		private int _currentQueueStep;

		public Fight(ICoroutineRunner coroutineRunner,
			List<EnemyAttackPresenter> enemyAttackPresenters,
			PlayerAttackPresenter playerAttackPresenter,
			IStepFightView stepFightView,
			DicePresenterAdapter dicePresenterAdapter,
			PlayerWeaponPanel elementsDamagePanel,
			GameObject popupReady,
			CustomButton customButtonReady)
		{
			_coroutineRunner = coroutineRunner;
			_enemyAttackPresenters = enemyAttackPresenters;
			_playerAttackPresenter = playerAttackPresenter;
			_stepFightView = stepFightView;
			_dicePresenterAdapter = dicePresenterAdapter;
			_elementsDamagePanel = elementsDamagePanel;
			_popupReady = popupReady;
			_customButtonReady = customButtonReady;

			_customButtonReady.onClick.AddListener(GetUserAnswer);
			_unitsOfQueue = new Queue<UnitAttackPresenter>();

			SubscribeOnDieEnemies();
			_playerAttackPresenter.Unit.Died += ActionAfterDie;

			_generatorAttackSteps = new GeneratorAttackSteps();
			_generatorAttackSteps.GenerateAttackingSteps(_enemyAttackPresenters, _playerAttackPresenter, _unitsOfQueue, _countSteps, _stepFightView);
		}

		public event Action FightEnded;

        public event Action ShowDice;

        public event Action HideDice;

		public void Dispose()
		{
			HideDice?.Invoke();
			_customButtonReady.onClick.RemoveListener(GetUserAnswer);
			_dicePresenterAdapter.Dispose();
			_elementsDamagePanel.Dispose();

			UnSubscribeOnDieEnemies();
			_playerAttackPresenter.Unit.Died -= ActionAfterDie;
		}

		public void Start()
		{
			if (_coroutine != null)
				_coroutineRunner.StopCoroutine(_coroutine);

			_coroutine = _coroutineRunner.StartCoroutine(AnimateAttackCoroutine());
		}

		private IEnumerator AnimateAttackCoroutine()
		{
			WaitUntil getUserAnswer = new WaitUntil(() => _userIsReady);
			_currentQueueStep = 0;

			_playerAttackPresenter.ShowAnimation(AnimationState.Idle);

			foreach (UnitAttackPresenter enemyAttackPresenter in _enemyAttackPresenters)
				enemyAttackPresenter.ShowAnimation(AnimationState.Idle);

			_dicePresenterAdapter.SetDisactive();
			_popupReady.gameObject.SetActive(true);
			_stepFightView.Show();
			_stepFightView.ActiveFrame(_currentQueueStep);

			yield return getUserAnswer;

			_elementsDamagePanel.Init(_playerAttackPresenter.Inventory.InventoryModel.GetWeapon());

			while (_enemyAttackPresenters.Count > 0 && _playerAttackPresenter.Unit.IsDie == false)
			{
				if (_unitsOfQueue.Count <= 0)
				{
					_generatorAttackSteps.GenerateAttackingSteps(_enemyAttackPresenters, _playerAttackPresenter, _unitsOfQueue, _countSteps, _stepFightView);
					_currentQueueStep = 0;
					_stepFightView.ActiveFrame(_currentQueueStep);
				}

				UnitAttackPresenter unitAttackPresenter = _unitsOfQueue.Dequeue();

				_dicePresenterAdapter.SetDisactive();

				if (unitAttackPresenter.Unit is Player player)
					UsePlayerAttack();
				else if (unitAttackPresenter.Unit is Enemy.Enemy enemy)
					UseEnemyAttack(unitAttackPresenter, enemy);

				_currentQueueStep++;
				_stepFightView.ActiveFrame(_currentQueueStep);
			}

			_coroutineRunner.StopCoroutine(_coroutine);
			_coroutine = null;
			_stepFightView.Hide();
			FightEnded?.Invoke();
		}

		private void GetUserAnswer()
		{
			_popupReady.gameObject.SetActive(false);
			_userIsReady = true;
		}

		private IEnumerator StartSingleAnimationCoroutine(AnimationState animationState,
			UnitAttackPresenter attackPresenter)
		{
			attackPresenter.ShowAnimation(animationState);

			if (attackPresenter is EnemyAttackPresenter enemyAttackPresenter)
				yield return new WaitUntil(() => enemyAttackPresenter.EnemyAttackView.IsComplete);
			else if (attackPresenter is PlayerAttackPresenter playerAttackPresenter)
				yield return new WaitUntil(() => playerAttackPresenter.PlayerAttackView.IsComplete);
		}

		private IEnumerator StartMultipleAnimationCoroutine(AnimationState animationState,
			List<EnemyAttackPresenter> attackPresenters)
		{
			attackPresenters.ForEach(unitAttackPresenter => unitAttackPresenter.ShowAnimation(animationState));

			yield return new WaitUntil(() => attackPresenters.All(unitAttackPresenter => unitAttackPresenter.EnemyAttackView.IsComplete));
		}

		private void SubscribeOnDieEnemies()
		{
			foreach (UnitAttackPresenter unitAttackPresenter in _enemyAttackPresenters)
				unitAttackPresenter.Unit.Died += ActionAfterDie;
		}

		private void UnSubscribeOnDieEnemies()
		{
			foreach (UnitAttackPresenter unitAttackPresenter in _enemyAttackPresenters)
				unitAttackPresenter.Unit.Died -= ActionAfterDie;
		}

		private IEnumerator UseEnemyAttack(UnitAttackPresenter unitAttackPresenter, Enemy.Enemy enemy)
		{
			yield return _coroutineRunner.StartCoroutine(StartSingleAnimationCoroutine(AnimationState.Attack, unitAttackPresenter));
			unitAttackPresenter.ShowAnimation(AnimationState.Idle);

			_playerAttackPresenter.Unit.TakeDamage(enemy.Weapon, false, enemy.IsBoss);

			yield return _coroutineRunner.StartCoroutine(StartSingleAnimationCoroutine(AnimationState.Hit, _playerAttackPresenter));

			if (_playerAttackPresenter.Unit.IsDie)
				_playerAttackPresenter.ShowAnimation(AnimationState.Dei);
			else
				_playerAttackPresenter.ShowAnimation(AnimationState.Idle);
		}

		private IEnumerator UsePlayerAttack()
		{
			WaitUntil waitDiceChooseUntil = new WaitUntil(_dicePresenterAdapter.CheckOnDicesShuffeled);
			EnemyViewChooser enemyChooser = new EnemyViewChooser(_elementsDamagePanel);

			foreach (EnemyAttackPresenter enemyAttackPresenter in _enemyAttackPresenters)
				enemyAttackPresenter.EnemyAttackView.PlayParticleEffect();

			yield return new WaitUntil(enemyChooser.TryChooseEnemy);

			foreach (EnemyAttackPresenter enemyAttackPresenter in _enemyAttackPresenters)
				enemyAttackPresenter.EnemyAttackView.StopParticleEffect();

			_dicePresenterAdapter.SetActive(enemyChooser.Weapon.ChanceToSplash.ToString(),
				enemyChooser.Weapon.ChanceToCritical.ToString(),
				enemyChooser.Weapon.ChanceToModifier.ToString());

			ShowDice?.Invoke();
			yield return waitDiceChooseUntil;
			HideDice?.Invoke();

			_dicePresenterAdapter.RestartShuffelValue();

			yield return _coroutineRunner.StartCoroutine(StartSingleAnimationCoroutine(AnimationState.Attack, _playerAttackPresenter));
			_playerAttackPresenter.ShowAnimation(AnimationState.Idle);

			bool isSplashAttack = enemyChooser.Weapon.ChanceToSplash == _dicePresenterAdapter.LeftDiceValue;
			bool isCritical = enemyChooser.Weapon.ChanceToCritical == _dicePresenterAdapter.CenterDiceValue;
			bool isModification = enemyChooser.Weapon.ChanceToModifier == _dicePresenterAdapter.RightDiceValue;

			if (isSplashAttack)
			{
				List<EnemyAttackPresenter> allLiveEnemy = _enemyAttackPresenters.Where(x => x.Unit.IsDie == false).ToList();

				yield return _coroutineRunner.StartCoroutine(StartMultipleAnimationCoroutine(AnimationState.Hit, allLiveEnemy));

				foreach (UnitAttackPresenter enemy in allLiveEnemy)
				{
					enemy.Unit.TakeDamage(enemyChooser.Weapon, isCritical, isModification);

					if (enemy.Unit.IsDie)
						enemy.ShowAnimation(AnimationState.Dei);
					else
						enemy.ShowAnimation(AnimationState.Idle);
				}
			}
			else
			{
				UnitAttackPresenter enemy = _enemyAttackPresenters.FirstOrDefault(x => x.EnemyAttackView.Guid == enemyChooser.AttackView.Guid);

				yield return _coroutineRunner.StartCoroutine(StartSingleAnimationCoroutine(AnimationState.Hit, enemy));

				enemy.Unit.TakeDamage(enemyChooser.Weapon, isCritical, isModification);

				if (enemy.Unit.IsDie)
					enemy.ShowAnimation(AnimationState.Dei);
				else
					enemy.ShowAnimation(AnimationState.Idle);
			}

			enemyChooser.Dispose();
		}

		private void ActionAfterDie(Unit unit)
		{
			if (_playerAttackPresenter.Unit == unit)
			{
				StartSingleAnimationCoroutine(AnimationState.Dei, _playerAttackPresenter);
			}
			else
			{
				EnemyAttackPresenter enemyPresenter = _enemyAttackPresenters.FirstOrDefault(x => x.Unit == unit);
				_enemyAttackPresenters.Remove(enemyPresenter);

				_generatorAttackSteps.GenerateAttackingSteps(_enemyAttackPresenters, _playerAttackPresenter, _unitsOfQueue, _countSteps, _stepFightView);
				_currentQueueStep = -1;
				_coroutineRunner.StartCoroutine(StartSingleAnimationCoroutine(AnimationState.Dei, enemyPresenter));
				enemyPresenter.EnemyAttackView.HideViewObject();
			}
		}
	}
}