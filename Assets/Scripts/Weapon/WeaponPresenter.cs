namespace Assets.Weapon
{
    public class WeaponPresenter
    {
        private readonly WeaponView _weaponView;
        private readonly Weapon _weapon;

        public WeaponPresenter(WeaponView weaponView, Weapon weapon)
        {
            _weaponView = weaponView;
            _weapon = weapon;
            SetStartValues();
        }


        public void PlayAnimation()
        {
            
        }
        
        private void SetStartValues()
        {
            _weaponView.DamageData = _weapon.DamageData;
            _weaponView.ChanceToSplash = _weapon.ChanceToSplash;
            _weaponView.MinValueToCriticalDamage = _weapon.MinValueToCriticalDamage;
            _weaponView.ValueModifier = _weapon.ValueModifier;
        }
    }
}