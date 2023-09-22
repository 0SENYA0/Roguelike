using System;
using Assets.Interface;
using Assets.TeachingLevels.Cases;
using TMPro;
using UnityEngine;

namespace Assets.TeachingLevels
{
    public class TrainingInfo : MonoBehaviour, ICoroutineRunner
    {
        [SerializeField] private PlayersTeacherView _playersTeacherView;
        [SerializeField] private TMP_Text _welcomeTMPText;
        private TeachingTextCase _welcomeText;

        private void OnEnable()
        {
            _welcomeText = new TeachingTextCase(_welcomeTMPText, this);
        }

        public void StartTeach()
        {
            _playersTeacherView.Show();
            //_welcomeText.EnterWaitUntilCase(Temp);
            //Show Player
            //Show Position 
        }

        private bool Temp()
        {
            return false;
        }
    }
}