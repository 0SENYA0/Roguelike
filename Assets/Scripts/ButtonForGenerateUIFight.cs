using System.Collections.Generic;
using Assets.Fight;
using UnityEngine;
using UnityEngine.UI;

namespace Assets
{
    [RequireComponent(typeof(Button))]
    public class ButtonForGenerateUIFight : MonoBehaviour
    {
        [SerializeField] private UIFight _uiFight;
        [SerializeField] private Player.Player _player;
        [SerializeField] private List<Enemy.Enemy> _enemies;

        private Button _button;

        private void Awake() =>
            _button = GetComponent<Button>();

        private void OnEnable() =>
            _button.onClick.AddListener(Generate);

        private void OnDisable() =>
            _button.onClick.RemoveListener(Generate);

        private void Generate()
        {
            _uiFight.gameObject.SetActive(true);
            _uiFight.SetActiveFightPlace(_player, _enemies.ToArray());
        }
    }
}