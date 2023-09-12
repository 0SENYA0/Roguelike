using System.Linq;
using Assets.DefendItems;
using Assets.Scripts.UI.Widgets;
using UnityEngine;

namespace Assets.Inventory
{
    public class InventoryPresenter
    {
        private readonly InventoryView _inventoryView;
        private readonly InventoryModel _inventoryModel;

        private Armor _activeArmor;
        
        public InventoryPresenter(InventoryView inventoryView)
        {
            _inventoryView = inventoryView;
            _inventoryModel = new InventoryModel(_inventoryView.PlayerInventory.GetWeapons(),
                _inventoryView.PlayerInventory.GetArmors(), _inventoryView.PlayerInventory.MagicItems);

            _activeArmor = _inventoryModel.Armor.FirstOrDefault(x => x.Body != null);

            FillInvetoryView();
        }

        public InventoryModel InventoryModel => _inventoryModel;
        public InventoryView InventoryView => _inventoryView;
        public Armor ActiveArmor => _activeArmor;

        private void FillInvetoryView()
        {
            for (int i = 0; i < _inventoryModel.Armor.Length; i++)
            {
                int temp = i;
                _inventoryView.DefendItemLineView[temp].BodyDamage.text = _inventoryModel.Armor[temp].Body.Value.ToString();
                _inventoryView.DefendItemLineView[temp].ElementImage.sprite = _inventoryView.ElementsSpriteView.GetElementSprite(_inventoryModel.Armor[temp].Body.Element);
                _inventoryView.DefendItemLineView[temp].HeadDamage.text = _inventoryModel.Armor[temp].Head.Value.ToString();
                _inventoryView.DefendItemLineView[temp].CustomButton.onClick.AddListener(() => SetNewActiveArmor(_inventoryModel.Armor[temp], _inventoryView.DefendItemLineView[temp].CustomButton));
            }

            #region AttackItem
            Debug.Log("_inventoryModel = " +_inventoryModel.Weapon.Length);
            Debug.Log("_inventoryView = " + _inventoryView.AttackItemLineView.Count);

            for (int i = 0; i < _inventoryModel.Weapon.Length; i++)
            {
                _inventoryView.AttackItemLineView[i].Element.sprite = _inventoryView.ElementsSpriteView.GetElementSprite(_inventoryModel.Weapon[i].Element);
                _inventoryView.AttackItemLineView[i].InfoInLine.Damage.text = _inventoryModel.Weapon[i].Damage.ToString();
                _inventoryView.AttackItemLineView[i].InfoInLine.ValueModifier.text = _inventoryModel.Weapon[i].ValueModifier.ToString();
                _inventoryView.AttackItemLineView[i].InfoInLine.ChanceCriticalDamage.text = _inventoryModel.Weapon[i].MinValueToCriticalDamage.ToString();
                _inventoryView.AttackItemLineView[i].InfoInLine.ChanceToSplash.text = _inventoryModel.Weapon[i].ChanceToSplash.ToString();
            }
            #endregion
            #region MagickItem
            // for (int i = 0; i < _inventoryModel.Armor.Length; i++)
            // {
            //     _inventoryView.DefendItemLineView[i].BodyDamage.text = _inventoryModel.Armor[i].Body.Value.ToString();
            //     _inventoryView.DefendItemLineView[i].ElementImage.sprite = _inventoryView.ElementsSpriteView.GetElementSprite(_inventoryModel.Armor[i].Body.Element);
            //     _inventoryView.DefendItemLineView[i].HeadDamage.text = _inventoryModel.Armor[i].Head.Value.ToString();
            // }
            #endregion
        }

        private void SetNewActiveArmor(Armor armor, CustomButton customButton)
        {
            ActivateDefendItemLine();
            
            customButton.gameObject.SetActive(false);
            _activeArmor = armor;
        }

        private void ActivateDefendItemLine()
        {
            foreach (DefendItemLineView defendItemLineView in _inventoryView.DefendItemLineView)
            {
                defendItemLineView.gameObject.SetActive(true);
            }
        }
    }
}