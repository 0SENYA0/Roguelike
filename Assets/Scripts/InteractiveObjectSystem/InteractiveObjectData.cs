using Assets.Fight.Element;

namespace Assets.Scripts.InteractiveObjectSystem
{
    public class InteractiveObjectData
    {
        public InteractiveObjectData(string name, string data, Element element)
        {
            Name = name;
            Data = data;
            Element = element;
        }

        public string Name { get; }
        public string Data { get; }
        public Element Element { get; }
    }
}