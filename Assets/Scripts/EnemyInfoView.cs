using System.Linq;
using Assets.Config;
using Assets.Enemy;
using Assets.Scripts.InteractiveObjectSystem;
using TMPro;
using UnityEngine;

namespace Assets
{
	public class EnemyInfoView : InfoView
	{
		[SerializeField] private TMP_Text _lable;
		[SerializeField] private TMP_Text _damageInfo;
		[SerializeField] private TMP_Text _armorInfo;
		[SerializeField] private TMP_Text _countInfo;
		[SerializeField] private AttackAndDefendView _attackAndDefendViewFirst;
		[SerializeField] private AttackAndDefendView _attackAndDefendViewSecond;
		[SerializeField] private AttackAndDefendView _attackAndDefendViewThirsd;
		[SerializeField] private ElementsSpriteView _elementsSprite;

		public void Show(IEnemyPresenter enemyPresenter)
		{
			HideAttackAndDefendPanel();
			gameObject.SetActive(true);
			AddInfoTest(enemyPresenter);
			ShowCorrectStatsOfEnemies(enemyPresenter);
		}

		private void HideAttackAndDefendPanel()
		{
			_attackAndDefendViewFirst.Hide();
			_attackAndDefendViewSecond.Hide();
			_attackAndDefendViewThirsd.Hide();
		}

		private void AddInfoTest(IEnemyPresenter enemyPresenter)
		{
			_lable.text = enemyPresenter.Enemy.Select(x => x.IsBoss).FirstOrDefault()
				? $"{GetLocalizedText(LanguageConfig.BossKey)}: {enemyPresenter.EnemyView.Name}"
				: enemyPresenter.EnemyView.Name;

			_damageInfo.text = $"{GetLocalizedText(LanguageConfig.DamageKey)}: {enemyPresenter.Enemy.Select(x => x.Weapon).FirstOrDefault().Damage:F1}";

			int armorValue = (int)enemyPresenter.Enemy.Select(x => x.Armor).FirstOrDefault().Body.Value +
			                 (int)enemyPresenter.Enemy.Select(x => x.Armor).FirstOrDefault().Body.Value;

			_armorInfo.text = $"{GetLocalizedText(LanguageConfig.ArmorKey)}: {armorValue}";
			_countInfo.text = $"{GetLocalizedText(LanguageConfig.CountKey)}: {enemyPresenter.Enemy.Count}";
		}

		private void ShowCorrectStatsOfEnemies(IEnemyPresenter enemyPresenter)
		{
			for (int i = 0; i < enemyPresenter.Enemy.Count; i++)
			{
				switch (i)
				{
					case 0:
						_attackAndDefendViewFirst.Show();

						_attackAndDefendViewFirst.AttactElement.sprite =
							_elementsSprite.GetElementSprite(enemyPresenter.Enemy[i].Weapon.Element);
						_attackAndDefendViewFirst.DefendElement.sprite =
							_elementsSprite.GetElementSprite(enemyPresenter.Enemy[i].Armor.Body.Element);
						break;
					case 1:
						_attackAndDefendViewSecond.Show();

						_attackAndDefendViewSecond.AttactElement.sprite =
							_elementsSprite.GetElementSprite(enemyPresenter.Enemy[i].Weapon.Element);
						_attackAndDefendViewSecond.DefendElement.sprite =
							_elementsSprite.GetElementSprite(enemyPresenter.Enemy[i].Armor.Body.Element);
						break;
					case 2:
						_attackAndDefendViewThirsd.Show();

						_attackAndDefendViewThirsd.AttactElement.sprite =
							_elementsSprite.GetElementSprite(enemyPresenter.Enemy[i].Weapon.Element);
						_attackAndDefendViewThirsd.DefendElement.sprite =
							_elementsSprite.GetElementSprite(enemyPresenter.Enemy[i].Armor.Body.Element);
						break;
				}
			}
		}
	}
}