using DefaultNamespace.Tools;
using UnityEngine;

namespace DefaultNamespace
{
    public class Test : MonoBehaviour
    {
        private void Start()
        {
            for (int i = 0; i < 10; i++)
            {
                ConsoleTools.LogSuccess($"{Random.value}");
            }
        }
    }
}