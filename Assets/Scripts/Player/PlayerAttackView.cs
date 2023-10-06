using Assets.Person;

namespace Assets.Player
{
    public class PlayerAttackView : UnitAttackView
    {
        protected override void OnPastEnable()
        {
            base.OnPastEnable();
        }

        public override void ChangeUIHealthValue(float value)
        {
            
        }
    }
}