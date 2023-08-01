using Assets.Fight.Element;

namespace Assets.Weapon
{
    public struct DamageData
    {
        public DamageData(float value, Element element)
        {
            Value = value;
            Element = element;
        }

        public float Value { get; }
        public Element Element { get; }
    }
}