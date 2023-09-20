using System;
using Assets.Inventory;
using Assets.Person;
using UnityEngine;

namespace Assets.Utils
{
    public class EnemyViewChooser : IDisposable
    {
        private readonly PlayerWeaponPanel _weaponPanel;

        private UnitAttackView _attackView;
        private bool _haveClick;
        private Weapon.Weapon _activeWeapon;

        public EnemyViewChooser(PlayerWeaponPanel weaponPanel)
        {
            _weaponPanel = weaponPanel;

            _weaponPanel.ChooseWeaponEvent += ChooseWeapon;
            _weaponPanel.ClosePanelEvent += OnClosePanel;
        }

        public Weapon.Weapon Weapon => _activeWeapon;
        public UnitAttackView AttackView => _attackView;

        public bool TryChooseEnemy()
        {
            if (TryGetEnemyView(out UnitAttackView unitAttackView))
            {
                _attackView = unitAttackView;
                _weaponPanel.Show();
            }

            if (_haveClick)
            {
                _haveClick = false;
                return true;
            }

            return false;
        }

        public void Dispose()
        {
            _weaponPanel.ChooseWeaponEvent -= ChooseWeapon;
            _weaponPanel.ClosePanelEvent -= OnClosePanel;
        }

        private void OnClosePanel()
        {
            _weaponPanel.Hide();
        }

        private void ChooseWeapon(IInventoryItem obj)
        {
            _activeWeapon = obj as Weapon.Weapon;
            _haveClick = true;
            _weaponPanel.Hide();
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
                    data = selectedObject;
                    return true;
                }
            }

            return false;
        }
    }
}