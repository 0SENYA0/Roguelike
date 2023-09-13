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
        private IWeapon _weapon;

        public EnemyViewChooser(IElementsDamagePanel elementsDamagePanel, Player.Player player,
            List<EnemyAttackView> enemyAttackViews, ElementsSpriteView elementsSpriteView)
        {
            _elementsDamagePanel = elementsDamagePanel;
            _player = player;
            _enemyAttackViews = enemyAttackViews;
            _elementsSpriteView = elementsSpriteView;

            _elementsDamagePanel.Exit.onClick.AddListener(HidePanel);
            FillInfoElementInfoLine(player.InventoryPresenter.InventoryModel.Weapon[0],
                _elementsDamagePanel.FireElementInfoLine);
            _elementsDamagePanel.FireElementInfoLine.InfoInLine.ButtonAttack.onClick.AddListener(() =>
                GetTrue(player.InventoryPresenter.InventoryModel.Weapon[0]));

            FillInfoElementInfoLine(player.InventoryPresenter.InventoryModel.Weapon[1],
                _elementsDamagePanel.MetalElementInfoLine);
            _elementsDamagePanel.MetalElementInfoLine.InfoInLine.ButtonAttack.onClick.AddListener(() =>
                GetTrue(player.InventoryPresenter.InventoryModel.Weapon[1]));

            FillInfoElementInfoLine(player.InventoryPresenter.InventoryModel.Weapon[2],
                _elementsDamagePanel.StoneElementInfoLine);
            _elementsDamagePanel.StoneElementInfoLine.InfoInLine.ButtonAttack.onClick.AddListener(() =>
                GetTrue(player.InventoryPresenter.InventoryModel.Weapon[2]));

            FillInfoElementInfoLine(player.InventoryPresenter.InventoryModel.Weapon[3],
                _elementsDamagePanel.TreeElementInfoLine);
            _elementsDamagePanel.TreeElementInfoLine.InfoInLine.ButtonAttack.onClick.AddListener(() =>
                GetTrue(player.InventoryPresenter.InventoryModel.Weapon[3]));

            FillInfoElementInfoLine(player.InventoryPresenter.InventoryModel.Weapon[4],
                _elementsDamagePanel.WaterElementInfoLine);
            _elementsDamagePanel.WaterElementInfoLine.InfoInLine.ButtonAttack.onClick.AddListener(() =>
                GetTrue(player.InventoryPresenter.InventoryModel.Weapon[4]));
        }

        public IWeapon Weapon => _weapon;
        public UnitAttackView AttackView => _attackView;

        public void Dispose()
        {
            _elementsDamagePanel.FireElementInfoLine.InfoInLine.ButtonAttack.onClick.RemoveListener(() =>
                GetTrue(_player.InventoryPresenter.InventoryModel.Weapon[0]));
            _elementsDamagePanel.MetalElementInfoLine.InfoInLine.ButtonAttack.onClick.RemoveListener(() =>
                GetTrue(_player.InventoryPresenter.InventoryModel.Weapon[1]));
            _elementsDamagePanel.StoneElementInfoLine.InfoInLine.ButtonAttack.onClick.RemoveListener(() =>
                GetTrue(_player.InventoryPresenter.InventoryModel.Weapon[2]));
            _elementsDamagePanel.TreeElementInfoLine.InfoInLine.ButtonAttack.onClick.RemoveListener(() =>
                GetTrue(_player.InventoryPresenter.InventoryModel.Weapon[3]));
            _elementsDamagePanel.WaterElementInfoLine.InfoInLine.ButtonAttack.onClick.RemoveListener(() =>
                GetTrue(_player.InventoryPresenter.InventoryModel.Weapon[4]));
        }

        public bool TryChooseEnemy()
        {
            // foreach (EnemyAttackView enemyAttackView in _enemyAttackViews)
            // {
            //     enemyAttackView.ShowSelecterArea();
            // }
            //
            if (TryGetEnemyView(out UnitAttackView unitAttackView))
            {
                _attackView = unitAttackView;
                _elementsDamagePanel.ShowPanel();
            }

            if (_haveClick)
            {
                _haveClick = false;
                
                // foreach (EnemyAttackView enemyAttackView in _enemyAttackViews)
                // {
                //     enemyAttackView.HideSelecterArea();
                // }
                
                return true;
            }

            return false;
        }

        private void HidePanel() =>
            _elementsDamagePanel.HidePanel();

        // Переименовать
        private void GetTrue(IWeapon weapon)
        {
            _weapon = weapon;
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