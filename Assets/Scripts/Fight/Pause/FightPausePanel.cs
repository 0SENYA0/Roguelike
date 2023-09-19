using UnityEngine;

namespace Assets.Fight.Pause
{
    public class FightPausePanel : MonoBehaviour
    {
        public void Show()
        {
            gameObject.SetActive(true);
        }

        public void Hide()
        {
            gameObject.SetActive(false);
        }
    }
}