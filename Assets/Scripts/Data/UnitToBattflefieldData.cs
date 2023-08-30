using System.Collections.Generic;
using Assets.Enemy;
using Assets.Player;

namespace Assets.Data
{
    public class UnitToBattflefieldData
    {
        private UnitToBattflefieldData()
        {
        }

        private static UnitToBattflefieldData _instance;

        public static UnitToBattflefieldData Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new UnitToBattflefieldData();

                return _instance;
            }
        }
        
        public PlayerPresenter PlayerPresenter { get; set; }
        public IReadOnlyList<EnemyPresenter> EnemyPresenters { get; set; }
    }
}