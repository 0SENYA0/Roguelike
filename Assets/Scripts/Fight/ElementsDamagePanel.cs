using UnityEngine;
using UnityEngine.UI;

namespace Assets.Fight
{
    public class ElementsDamagePanel : MonoBehaviour, IElementsDamagePanel
    {
        [SerializeField] private Button _exit;
        [SerializeField] private ElementInfoLine _fireElementInfoLine;
        [SerializeField] private ElementInfoLine _treeElementInfoLine;
        [SerializeField] private ElementInfoLine _waterElementInfoLine;
        [SerializeField] private ElementInfoLine _metalElementInfoLine;
        [SerializeField] private ElementInfoLine _stoneElementInfoLine;

        public Button Exit => _exit;

        public IElementInfoLine FireElementInfoLine => _fireElementInfoLine;

        public IElementInfoLine TreeElementInfoLine => _treeElementInfoLine;

        public IElementInfoLine WaterElementInfoLine => _waterElementInfoLine;

        public IElementInfoLine MetalElementInfoLine => _metalElementInfoLine;

        public IElementInfoLine StoneElementInfoLine => _stoneElementInfoLine;

        private void OnEnable() =>
            _exit.onClick.AddListener(HidePanel);

        private void OnDisable() =>
            _exit.onClick.RemoveListener(HidePanel);

        public void HidePanel() =>
            gameObject.SetActive(false);

        public void ShowPanel() =>
            gameObject.SetActive(true);
    }
}