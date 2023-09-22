using System;
using AnimationState = Assets.Scripts.AnimationComponent.AnimationState;

namespace Assets.Person
{
    public abstract class UnitAttackPresenter : IDisposable
    {
        private  readonly Unit _unit;
        private  readonly UnitAttackView _unitAttackView;
        private readonly float _baseHealth;

        public UnitAttackPresenter(Unit unit, UnitAttackView unitAttackView)
        {
            _unit = unit;
            _unitAttackView = unitAttackView;
            FillDataForClips();
            _baseHealth = unit.Health;
            _unit.HealthChanged += ChangeUIHealthValue;
        }

        public Unit Unit => _unit;
        
        private void FillDataForClips() =>
            _unitAttackView.FillDataForClips(_unit.SpriteAnimation.AnimationClips);

        public void ShowAnimation(AnimationState animationState) =>
            _unitAttackView.SetClip(animationState);

        public void Dispose() => 
            _unit.HealthChanged -= ChangeUIHealthValue;
        
        private void ChangeUIHealthValue(float health)
        {
            float result = health / _baseHealth;
            _unitAttackView.ChangeUIHealthValue(result);
        }
    }
}