using System.Collections.Generic;
using Assets.UI.TrainingPanel;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.GUI_Kit_Casual_Game.Scripts
{
    public class PanelControl : MonoBehaviour
    {
        [SerializeField] private List<TrainingItem> _panels;
        [SerializeField] private Button _buttonPrev;
        [SerializeField] private Button _buttonNext;
        [SerializeField] private TextMeshProUGUI _textTitle;
        
        private int _page = 0;

        private void OnEnable()
        {
            _buttonPrev.onClick.AddListener(ClickPrev);
            _buttonNext.onClick.AddListener(ClickNext);
            _page = 0;
            
            _panels[_page].gameObject.SetActive(true);
            _textTitle.text = _panels[_page].Label;

            SetArrowActive();
        }

        private void OnDisable()
        {
            _panels[_page].gameObject.SetActive(false);
            _buttonPrev.onClick.RemoveListener(ClickPrev);
            _buttonNext.onClick.RemoveListener(ClickNext);
        }

        private void ClickPrev()
        {
            if (_page <= 0) return;

            _panels[_page].gameObject.SetActive(false);
            _panels[_page -= 1].gameObject.SetActive(true);
            _textTitle.text = _panels[_page].Label;
            SetArrowActive();
        }
        
        private void ClickNext()
        {
            if (_page >= _panels.Count - 1) return;

            _panels[_page].gameObject.SetActive(false);
            _panels[_page += 1].gameObject.SetActive(true);
            _textTitle.text = _panels[_page].Label;
            SetArrowActive();
        }

        private void SetArrowActive()
        {
            _buttonPrev.gameObject.SetActive(_page > 0);
            _buttonNext.gameObject.SetActive(_page < _panels.Count - 1);
        }
    }
}
