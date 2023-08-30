using Assets.UI;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Assets
{
    [RequireComponent(typeof(Button))]
    public class ButtonBackMenu : MonoBehaviour
    {
        private Button _button;

        private void Awake()
        {
            _button = GetComponent<Button>();
        }

        private void OnEnable()
        {
            _button.onClick.AddListener(LoadMainMenu);
        }

        private void OnDisable()
        {
            _button.onClick.RemoveListener(LoadMainMenu);
        }

        private void LoadMainMenu()
        {
            Curtain.Instance.ShowAnimation(() => {SceneManager.LoadScene("Menu");});
        }
    }
}