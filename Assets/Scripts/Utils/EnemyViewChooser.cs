using System;
using Assets.Enemy;
using Assets.Inventory;
using Assets.Person;
using UnityEngine;

namespace Assets.Utils
{
    public class EnemyViewChooser : IDisposable
    {
        private readonly PlayerWeaponPanel _weaponPanel;
        private readonly int _mouseKey = 0;

        private EnemyAttackView _attackView;
        private bool _haveClick;
        private Camera _camera;
        private Weapon.Weapon _activeWeapon;

        public EnemyViewChooser(PlayerWeaponPanel weaponPanel)
        {
            _camera = Camera.main;
            
            _weaponPanel = weaponPanel;

            _weaponPanel.ChooseWeaponEvent += ChooseWeapon;
            _weaponPanel.ClosePanelEvent += OnClosePanel;
        }

        public Weapon.Weapon Weapon => _activeWeapon;
        public EnemyAttackView AttackView => _attackView;

        public bool TryChooseEnemy()
        {
            if (TryGetEnemyView(out EnemyAttackView unitAttackView))
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

        private bool TryGetEnemyView(out EnemyAttackView data)
        {
            data = null;

            if (Input.GetMouseButtonDown(_mouseKey))
            {
                Ray ray = _camera.ScreenPointToRay(Input.mousePosition);
                RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction, Mathf.Infinity);

                if (hit.collider != null && hit.collider.TryGetComponent(out EnemyAttackView selectedObject))
                {
                    data = selectedObject;
                    return true;
                }
            }

            return false;
        }
    }
}