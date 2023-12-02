using Assets.Person;
using TMPro;
using UnityEngine;

namespace Assets.UI.HUD
{
    public class InventoryHUDView : MonoBehaviour
    {
        [SerializeField] private PlayerView _inventory;
        [SerializeField] private TMP_Text _text;

        private const float ArtificialDelayBeforeShowing = 1.5f;

        private void Start() =>
            Invoke(nameof(InitSubscribe), ArtificialDelayBeforeShowing);

        private void OnDestroy() =>
            _inventory.InventoryPresenter.InventoryModel.CountItemsChangeEvent -= OnCountItemsChange;

        private void InitSubscribe()
        {
            _inventory.InventoryPresenter.InventoryModel.CountItemsChangeEvent += OnCountItemsChange;
            OnCountItemsChange(_inventory.InventoryPresenter.InventoryModel.MaxSize,
                _inventory.InventoryPresenter.InventoryModel.TotalSize);
        }

        private void OnCountItemsChange(int maxCount, int currentCount) =>
            _text.text = $"{currentCount}/{maxCount}";
    }
}