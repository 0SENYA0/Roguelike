using System.Collections.Generic;
using UnityEngine;

namespace Assets.UI.TrainingPanel
{
    [CreateAssetMenu(fileName = "TrainingInformation", menuName = "ScriptableObject/TrainingInformation", order = 0)]
    public partial class TrainingInformation : ScriptableObject
    {
        [SerializeField] private List<Info> _information;

        public List<Info> Information => _information;
    }
}