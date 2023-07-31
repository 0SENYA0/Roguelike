namespace Assets.Enemy
{
    public class EnemyFirstLevelBuilder : IEnemyBuilder
    {
        private readonly EnemyFactory _enemyFactory;

        public EnemyFirstLevelBuilder() =>
            _enemyFactory = new EnemyFactory();

        public Enemy BuildMedic() =>
            throw new System.NotImplementedException();

        /// <summary>
        /// Bot
        /// </summary>
        /// <returns></returns>
        public Enemy BuildEasyUnit()
        {
            Enemy enemy = _enemyFactory.Create(null);
            // TODO add parameters for type enemy Bot 
            return null;
        }

        /// <summary>
        /// DaggerMush
        /// </summary>
        /// <returns></returns>
        public Enemy BuildNormalUnit()
        {
            Enemy enemy = _enemyFactory.Create(null);
            // TODO add parameters for type enemy DaggerMush 
            return null;
        }

        /// <summary>
        /// FinalBoss
        /// </summary>
        /// <returns></returns>
        public Enemy BuildBoss()
        {
            Enemy enemy = _enemyFactory.Create(null);
            enemy.MakeBoss();
            // TODO add parameters for type enemy Boss 
            return null;
        }
    }

}