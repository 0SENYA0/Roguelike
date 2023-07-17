using Player;
using UnityEngine;
using UnityEngine.UI;

namespace Fight
{
    public class PlayerStep : MonoBehaviour
    {
        [SerializeField] private Button _singleAttackButton;
        [SerializeField] private Button _splashAttackButton;
        [SerializeField] private Button _healButton;
        [SerializeField] private Button _endStepButton;

        private void OnEnable()
        {
            _singleAttackButton.onClick.AddListener(() => SingleAttack());
            _splashAttackButton.onClick.AddListener(() => Splash());
            _healButton.onClick.AddListener(() => Heal());
            _endStepButton.onClick.AddListener(() => EndStep());
        }

        private void OnDisable()
        {
            _singleAttackButton.onClick.RemoveListener(() => SingleAttack());
            _splashAttackButton.onClick.RemoveListener(() => Splash());
            _healButton.onClick.RemoveListener(() => Heal());
            _endStepButton.onClick.RemoveListener(() => EndStep());
        }

        public void SingleAttack()
        {
            PlayerSingleAttackObserver.Instance.Notify();
            Debug.Log("Select enemy");
            Debug.Log("Attack");
        }

        public void Splash()
        {
            Debug.Log("Splash Attack");
        }

        public void Heal()
        {
            Debug.Log("Heal Player");
        }

        public void EndStep()
        {
            Debug.Log("End Step");
        }
    }
}