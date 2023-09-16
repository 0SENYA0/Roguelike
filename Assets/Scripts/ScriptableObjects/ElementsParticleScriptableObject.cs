using System.Collections.Generic;
using UnityEngine;

namespace Assets.ScriptableObjects
{
    [CreateAssetMenu(fileName = "ElementsParticle", menuName = "ScriptableObject/ElementsParticle", order = 0)]
    public class ElementsParticleScriptableObject : ScriptableObject
    {
        [SerializeField] private List<ParticleSystem> _fire;
        [SerializeField] private List<ParticleSystem> _tree;
        [SerializeField] private List<ParticleSystem> _water;
        [SerializeField] private List<ParticleSystem> _metal;
        [SerializeField] private List<ParticleSystem> _stone;

        public IReadOnlyList<ParticleSystem> Fire => _fire;
        public IReadOnlyList<ParticleSystem> Tree => _tree;
        public IReadOnlyList<ParticleSystem> Water => _water;
        public IReadOnlyList<ParticleSystem> Metal => _metal;
        public IReadOnlyList<ParticleSystem> Stone => _stone;
        public IReadOnlyList<IReadOnlyList<ParticleSystem>> AllParticles => new[] { _fire, _tree, _water, _metal, _stone };
    }
}