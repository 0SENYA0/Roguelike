using UnityEngine;

namespace Assets.Interface
{
    public abstract class TypeElement
    {
    }

    public abstract class Element 
    {
        public Element(ParticleSystem particleSystem)
        {
        }

        public abstract void Execute();
    }

    public class ElementManager
    {
        private readonly FireElement _fire;
        private readonly MetalElement _metal;
        private readonly TreeElement _tree;
        private readonly StoneElement _stone;
        private readonly WaterElement _water;

        public ElementManager(FireElement fire, MetalElement metal, TreeElement tree, StoneElement stone, WaterElement water)
        {
            _fire = fire;
            _metal = metal;
            _tree = tree;
            _stone = stone;
            _water = water;
        }

        public void Interract(Element first, Element second)
        {
               
        }

        private void HighDamage()
        {
            
        }
        
        private void LowDamage()
        {
            
        }
    }

    /// <summary>
    /// Огонь, земля, металл, вода, дерево. Этими стихиями обладает оружие, предметы, заклинания.
    /// В зависимости, какие стихии взаимодействуют, урон повышается, понижается или не изменяется.
    /// </summary>

    public class MetalElement : Element
    {
        private readonly ParticleSystem _particleSystem;

        public MetalElement(ParticleSystem particleSystem) : base(particleSystem)
        {
            _particleSystem = particleSystem;
        }

        public override void Execute() =>
            _particleSystem.Play();
    }
    
    public class TreeElement : TypeElement
    {
    }

    public class StoneElement : TypeElement
    {
    }

    public class WaterElement : TypeElement
    {
    }

    public class FireElement : TypeElement
    {
    }
}