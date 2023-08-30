using System.Collections.Generic;
using Assets.Enemy;
using Assets.Person;

namespace Assets.Player
{
    public class PlayerFightAdapter
    {
        private readonly PlayerPresenter _presenter;
        private readonly UnitFightView _playerFightView;

        public PlayerFightAdapter(PlayerPresenter playerPresenter, UnitFightView playerFightView)
        {
            _presenter = playerPresenter;
            _playerFightView = playerFightView;
            FillPlayerFightPresenter();
        }

        public PlayerFightPresenter PlayerFightPresenter { get; private set; }

        private void FillPlayerFightPresenter() =>
            PlayerFightPresenter = new PlayerFightPresenter(_presenter.Player, _playerFightView);
    }
}