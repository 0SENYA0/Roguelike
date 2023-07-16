using UnityEngine;

namespace Fight
{
    public class FightPlace : MonoBehaviour
    {
        [SerializeField] private OneEnemyPlace _oneEnemyPlace;
        [SerializeField] private TwoEnemyPlace _twoEnemyPlace;
        [SerializeField] private ThreeEnemiesPlace _threeEnemiesPlace;
        [SerializeField] private BossEnemyPlace _bossEnemyPlace;

        public void SetOneEnemyPlace()
        {
            _oneEnemyPlace.gameObject.SetActive(true);

            DisactiveEnemyPlace(_twoEnemyPlace.gameObject, _threeEnemiesPlace.gameObject, _bossEnemyPlace.gameObject);
        }

        public void SetTwoEnemyPlace()
        {
            _twoEnemyPlace.gameObject.SetActive(true);

            DisactiveEnemyPlace(_oneEnemyPlace.gameObject, _threeEnemiesPlace.gameObject, _bossEnemyPlace.gameObject);
        }

        public void SetThreeEnemyPlace()
        {
            _threeEnemiesPlace.gameObject.SetActive(true);

            DisactiveEnemyPlace(_oneEnemyPlace.gameObject, _twoEnemyPlace.gameObject, _bossEnemyPlace.gameObject);
        }

        public void SetBossEnemyPlace()
        {
            _bossEnemyPlace.gameObject.SetActive(true);

            DisactiveEnemyPlace(_oneEnemyPlace.gameObject, _twoEnemyPlace.gameObject, _threeEnemiesPlace.gameObject);
        }

        private void DisactiveEnemyPlace(params GameObject[] enemyPlaces)
        {
            foreach (GameObject enemyPlace in enemyPlaces)
                enemyPlace.SetActive(false);
        }
    }
}