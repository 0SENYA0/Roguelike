using System;
using TMPro;
using UnityEngine;

namespace Assets.TimerSystem
{
    public class TimerView : MonoBehaviour
    {
        [SerializeField] private Timer _timer;
        [SerializeField] private TMP_Text _text;

        private void Update()
        {
            TimeSpan time = TimeSpan.FromSeconds(_timer.TimePerSeconds);
            _text.text = time.ToString(@"mm\:ss");
        }
    }
}