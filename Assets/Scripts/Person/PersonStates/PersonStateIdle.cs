using Assets.Scripts.EnemyScripts;

namespace Assets.Person.PersonStates
{
    public class PersonStateIdle : PersonState
    {
        public override void Enter() =>
            SpriteAnimation.SetClip(AnimationState.Idle);

    }
}