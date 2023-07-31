namespace Assets.Scripts.InteractiveObjectSystem
{
    public class InteractiveObjectData
    {
        public string Name { get; private set; }
        public string Data { get; private set; }

        public InteractiveObjectData(string name, string data)
        {
            Name = name;
            Data = data;
        }
    }
}