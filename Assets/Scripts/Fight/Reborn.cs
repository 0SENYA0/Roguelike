using System;
using Assets.Config;
using Assets.Infrastructure;
using Assets.Scripts.InteractiveObjectSystem;
using Assets.UI;
using Assets.User;
using UnityEngine;

namespace Assets.Fight
{
	public class Reborn
	{
		private readonly IPlayerPresenter _playerPresenter;
		private readonly GameObject _globalMap;
		private readonly GameObject _battlefieldMap;
		private readonly InteractiveObjectHandler _interactiveObjectHandler;

		public Reborn(IPlayerPresenter playerPresenter,
			GameObject globalMap,
			GameObject battlefieldMap,
			InteractiveObjectHandler interactiveObjectHandler)
		{
			_playerPresenter = playerPresenter;
			_globalMap = globalMap;
			_battlefieldMap = battlefieldMap;
			_interactiveObjectHandler = interactiveObjectHandler;
		}

		public void RebornWithIdol()
		{
			if (Game.GameSettings.PlayerData.Idol <= 0)
				throw new Exception($"The player contains {Game.GameSettings.PlayerData.Idol} idols for reborn");

			Game.GameSettings.PlayerData.Idol--;
			_playerPresenter.Player.Reborn();
			_playerPresenter.Player.Heal(PlayerHealth.MaxPlayerHealth / 2);

			Curtain.Instance.ShowAnimation(() =>
			{
				_globalMap.SetActive(true);
				_battlefieldMap.SetActive(false);
				_interactiveObjectHandler.ReturnToGlobalMap();
			});
		}

		public void RebornWithAD()
		{
			_playerPresenter.Player.Reborn();
			_playerPresenter.Player.Heal(PlayerHealth.MaxPlayerHealth / 2);

			Curtain.Instance.ShowAnimation(() =>
			{
				_globalMap.SetActive(true);
				_battlefieldMap.SetActive(false);
				_interactiveObjectHandler.ReturnToGlobalMap();
			});
		}
	}
}