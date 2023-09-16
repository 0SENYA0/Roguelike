using Assets.Inventory;
using UnityEngine;

namespace Assets.DefendItems
{
    public class Armor: IInventoryItem
    {
        private bool _isSelect;
        
        public Armor(Body body, Head head, ParticleSystem particleSystem)
        {
            Body = body;
            Head = head;
            ParticleSystem = particleSystem;
            _isSelect = false;
        }

        public ParticleSystem ParticleSystem { get; private set; }
        public Head Head { get; private set; }
        public Body Body { get; private set; }
        public bool IsSelect => _isSelect;

        public void Select()
        {
            _isSelect = true;
        }

        public void UnSelect()
        {
            _isSelect = false;
        }
    }
}