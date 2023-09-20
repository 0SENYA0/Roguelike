using System;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Inventory.Panel
{
    public class PanelItem : MonoBehaviour
    {
        [SerializeField] protected Button _remove;

        protected IInventoryItem _item;

        public Action<IInventoryItem> OnItemClicked;

        public void OnDispose()
        {
            _remove.onClick.RemoveListener(OnClickRemove);
            Destroy(gameObject);
        }

        protected void OnClickRemove()
        {
            OnItemClicked?.Invoke(_item);
            Destroy(gameObject);
        }
    }
}