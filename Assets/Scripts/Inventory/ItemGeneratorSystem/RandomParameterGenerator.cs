using Assets.Fight.Element;
using Assets.ScriptableObjects;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Assets.Inventory.ItemGeneratorSystem
{
    public class RandomParameterGenerator
    {
        private readonly ElementsParticleScriptableObject _elementsParticle;
        
        public RandomParameterGenerator(ElementsParticleScriptableObject particle)
        {
            _elementsParticle = particle;
        }
        
        public float RandomValue(ListOfItemStatsRangesScriptableObject.Range range)
        {
            return Random.Range(range.MinValue, range.MaxValue);
        }

        public float RandomValue(float a, float b)
        {
            return Random.Range(a, b);
        }

        public Element RandomElement()
        {
            return (Element) Random.Range(0, 5);
        }

        public ParticleSystem RandomParticle()
        {
            var randomElement = _elementsParticle.AllParticles[Random.Range(0, _elementsParticle.AllParticles.Count)];
            int randomIndex = Random.Range(0, randomElement.Count);

            return randomElement[randomIndex];
        }

        public ParticleSystem RandomParticle(Element element)
        {
            switch (element)
            {
                case Element.Fire:
                    return _elementsParticle.Fire[Random.Range(0, _elementsParticle.Fire.Count)];
                case Element.Tree:
                    return _elementsParticle.Tree[Random.Range(0, _elementsParticle.Tree.Count)];
                case Element.Water:
                    return _elementsParticle.Water[Random.Range(0, _elementsParticle.Water.Count)];
                case Element.Metal:
                    return _elementsParticle.Metal[Random.Range(0, _elementsParticle.Metal.Count)];
                case Element.Stone:
                    return _elementsParticle.Stone[Random.Range(0, _elementsParticle.Stone.Count)];
            }

            return null;
        }
    }
}