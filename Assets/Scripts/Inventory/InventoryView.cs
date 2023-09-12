using System.Collections.Generic;
using Assets.Fight;
using Assets.ScriptableObjects;
using Assets.Scripts.InteractiveObjectSystem;
using UnityEngine;

namespace Assets.Inventory
{
    public class InventoryView : MonoBehaviour
    {
        [SerializeField] private PlayerInventoryScriptableObject _playerInventory;
        [SerializeField] private List<DefendItemLineView> _defendItemLineView;
        [SerializeField] private List<ElementInfoLine> _attackItemLineView;
        [SerializeField] private List<MagickItemLineView> _magickItemLineView;
        [SerializeField] private ElementsSpriteView _elementsSpriteView;

        public ElementsSpriteView ElementsSpriteView => _elementsSpriteView;

        public List<DefendItemLineView> DefendItemLineView => _defendItemLineView;
 
        public List<ElementInfoLine> AttackItemLineView => _attackItemLineView;

        public List<MagickItemLineView> MagickItemLineView => _magickItemLineView;

        public PlayerInventoryScriptableObject PlayerInventory => _playerInventory;

        public void kek()
        {
        }
    }

    public class MagickItemLineView : MonoBehaviour
    {
    }

    public class AttackItemLineView : MonoBehaviour
    {
    }
}