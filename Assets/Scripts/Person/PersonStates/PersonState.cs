using Assets.Scripts.EnemyScripts;
using UnityEngine;

namespace Assets.Person.PersonStates
{
    public abstract class PersonState : MonoBehaviour
    {
        [SerializeField] protected SpriteAnimation SpriteAnimation;

        public virtual void Enter()
        {
        }

        public virtual void Exit()
        {
        }
    }
}