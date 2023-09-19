using System;
using System.Collections.Generic;
using Assets.Enemy;
using Assets.Interface;
using Assets.Person;
using Assets.Player;
using Random = UnityEngine.Random;

namespace Assets.Fight
{
    public class GeneraterAttackSteps
    {
        public void GenerateAttackingSteps
        (
            List<EnemyAttackPresenter> enemyAttackPresenters,
            PlayerAttackPresenter playerAttackPresenter, 
            Queue<UnitAttackPresenter> unitsOfQueue, 
            int countSteps,
            IStepFightView stepFightView
        )
        {
            unitsOfQueue.Clear();
            List<UnitAttackPresenter> persons = new List<UnitAttackPresenter>();

            foreach (UnitAttackPresenter enemyAttackPresenter in enemyAttackPresenters)
                persons.Add(enemyAttackPresenter);

            persons.Add(playerAttackPresenter);

            int numberIdenticalUnits = 0;

            UnitAttackPresenter tempUnitAttackPresenter;
            UnitAttackPresenter newUnitAttackPresenter;
            Type typeUnit = null;

            for (int i = 0; i < countSteps; i++)
            {
                tempUnitAttackPresenter = persons[Random.Range(0, persons.Count)];

                if (typeUnit == null)
                    typeUnit = tempUnitAttackPresenter.GetType();

                if (typeUnit == tempUnitAttackPresenter.GetType())
                {
                    numberIdenticalUnits++;

                    if (numberIdenticalUnits >= 3)
                    {
                        numberIdenticalUnits = 0;

                        foreach (UnitAttackPresenter unitAttackPresenter in persons)
                        {
                            if (tempUnitAttackPresenter.GetType() != unitAttackPresenter.GetType())
                            {
                                tempUnitAttackPresenter = unitAttackPresenter;
                                numberIdenticalUnits = 1;
                                typeUnit = tempUnitAttackPresenter.GetType();
                                break;
                            }
                        }
                    }
                }

                newUnitAttackPresenter = tempUnitAttackPresenter;

                stepFightView.SetSprite(newUnitAttackPresenter.Unit.Sprite, i);
                unitsOfQueue.Enqueue(newUnitAttackPresenter);
            }
        }
    }
}