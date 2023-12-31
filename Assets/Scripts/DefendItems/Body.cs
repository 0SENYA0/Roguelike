using Assets.Fight.Element;
using System;

namespace Assets.DefendItems
{
    [Serializable]
    public class Body
    {
        public Body(float value, Element element)
        {
            Value = value;
            Element = element;
        }

        public float Value { get; }

        public Element Element { get; }
    }
}