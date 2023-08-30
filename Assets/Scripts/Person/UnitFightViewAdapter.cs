using Assets.Scripts.AnimationComponent;

namespace Assets.Person
{
    public class UnitFightViewAdapter
    {
        private readonly UnitFightView _unitFightView;
        private readonly SpriteAnimation _spriteAnimation;

        public UnitFightViewAdapter(UnitFightView unitFightView, SpriteAnimation spriteAnimation)
        {
            _unitFightView = unitFightView;
            _spriteAnimation = spriteAnimation;
            FillFightView();
        }

        private void FillFightView() =>
            _unitFightView.FillClips(_spriteAnimation.Clips);
    }
}