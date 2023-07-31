using Assets.Person.PersonStates;
using UnityEngine;

namespace Assets.Person
{
    public class UnitView : MonoBehaviour
    {
        [SerializeField] private Sprite _sprite;

        [SerializeField] private PersonStateMachine _personStateMachine;
    }
}