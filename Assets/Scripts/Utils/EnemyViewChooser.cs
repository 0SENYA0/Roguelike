using System;
using System.Collections.Generic;
using Assets.Enemy;
using Assets.Fight;
using Assets.Interface;
using Assets.Person;
using Assets.Scripts.InteractiveObjectSystem;
using UnityEngine;

namespace Assets.Utils
{
    public class EnemyViewChooser : IDisposable
    {
        private readonly IElementsDamagePanel _elementsDamagePanel;
        private readonly Player.Player _player;
        private readonly IReadOnlyList<EnemyAttackView> _enemyAttackViews;
        private readonly ElementsSpriteView _elementsSpriteView;
        private UnitAttackView _attackView;
        private bool _haveClick;
        private IReadOnlyList<Weapon.Weapon> _weapon;

        public EnemyViewChooser(IElementsDamagePanel elementsDamagePanel, Player.Player player,
            List<EnemyAttackView> enemyAttackViews, ElementsSpriteView elementsSpriteView)
        {
            _elementsDamagePanel = elementsDamagePanel;
            _player = player;
            _enemyAttackViews = enemyAttackViews;
            _elementsSpriteView = elementsSpriteView;

            _elementsDamagePanel.Exit.onClick.AddListener(HidePanel);
            
            _weapon = player.InventoryPresenter.InventoryModel.GetWeapon();
            FillInfoElementInfoLine(_weapon[0], _elementsDamagePanel.FireElementInfoLine);
            _elementsDamagePanel.FireElementInfoLine.InfoInLine.ButtonAttack.onClick.AddListener(() =>
                GetTrue(_weapon[0]));
            
            FillInfoElementInfoLine(_weapon[1], _elementsDamagePanel.MetalElementInfoLine);
            _elementsDamagePanel.MetalElementInfoLine.InfoInLine.ButtonAttack.onClick.AddListener(() =>
                GetTrue(_weapon[1]));
            
            FillInfoElementInfoLine(_weapon[2], _elementsDamagePanel.StoneElementInfoLine);
            _elementsDamagePanel.StoneElementInfoLine.InfoInLine.ButtonAttack.onClick.AddListener(() =>
                GetTrue(_weapon[2]));
            
            FillInfoElementInfoLine(_weapon[3],
                _elementsDamagePanel.TreeElementInfoLine);
            _elementsDamagePanel.TreeElementInfoLine.InfoInLine.ButtonAttack.onClick.AddListener(() =>
                GetTrue(_weapon[3]));
            
            FillInfoElementInfoLine(_weapon[4],
                _elementsDamagePanel.WaterElementInfoLine);
            _elementsDamagePanel.WaterElementInfoLine.InfoInLine.ButtonAttack.onClick.AddListener(() =>
                GetTrue(_weapon[4]));
        }

        public Weapon.Weapon Weapon => _weapon[0];
        public UnitAttackView AttackView => _attackView;

        public void Dispose()
        {
            _elementsDamagePanel.FireElementInfoLine.InfoInLine.ButtonAttack.onClick.RemoveListener(() =>
                GetTrue(_weapon[0]));
            _elementsDamagePanel.MetalElementInfoLine.InfoInLine.ButtonAttack.onClick.RemoveListener(() =>
                GetTrue(_weapon[1]));
            _elementsDamagePanel.StoneElementInfoLine.InfoInLine.ButtonAttack.onClick.RemoveListener(() =>
                GetTrue(_weapon[2]));
            _elementsDamagePanel.TreeElementInfoLine.InfoInLine.ButtonAttack.onClick.RemoveListener(() =>
                GetTrue(_weapon[3]));
            _elementsDamagePanel.WaterElementInfoLine.InfoInLine.ButtonAttack.onClick.RemoveListener(() =>
                GetTrue(_weapon[4]));
        }

        public bool TryChooseEnemy()
        {
            if (TryGetEnemyView(out UnitAttackView unitAttackView))
            {
                _attackView = unitAttackView;
                _elementsDamagePanel.ShowPanel();
            }

            if (_haveClick)
            {
                _haveClick = false;
                return true;
            }

            return false;
        }

        private void HidePanel() =>
            _elementsDamagePanel.HidePanel();

        // Переименовать
        private void GetTrue(Weapon.Weapon weapon)
        {
            //_weapon = weapon;
            _haveClick = true;
            HidePanel();
        }

        private bool TryGetEnemyView(out UnitAttackView data)
        {
            data = null;

            if (Input.GetMouseButtonDown(0))
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction, Mathf.Infinity);

                if (hit.collider != null && hit.collider.TryGetComponent(out UnitAttackView selectedObject))
                {
                    Debug.Log(hit.collider.gameObject.name);
                    data = selectedObject;
                    return true;
                }
            }

            return false;
        }

        private void FillInfoElementInfoLine(IWeapon weapon, IElementInfoLine elementInfoLine)
        {
            elementInfoLine.Element.sprite = _elementsSpriteView.GetElementSprite(weapon.Element);
            elementInfoLine.InfoInLine.Damage.text = weapon.Damage.ToString();
            elementInfoLine.InfoInLine.ValueModifier.text = weapon.ValueModifier.ToString();
            elementInfoLine.InfoInLine.ChanceCriticalDamage.text = weapon.MinValueToCriticalDamage.ToString();
            elementInfoLine.InfoInLine.ChanceToSplash.text = weapon.ChanceToSplash.ToString();
        }
    }
}