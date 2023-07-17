using System;

namespace Player
{
    public class PlayerSingleAttackObserver
    {
        private PlayerSingleAttackObserver()
        {
            
        }

        private static PlayerSingleAttackObserver _instance;

        public static PlayerSingleAttackObserver Instance {
            get
            {
                if (_instance == null)
                {
                    _instance = new PlayerSingleAttackObserver();
                }

                return _instance;
            }
        }

        private event Action Attacked;
        
        public void Register(Action method) => 
            Attacked += method;

        public void Unregister(Action method) => 
            Attacked -= method;

        public void Notify() => 
            Attacked?.Invoke();
    }
}