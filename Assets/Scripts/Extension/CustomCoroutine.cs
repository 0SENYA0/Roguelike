using System.Collections;
using UnityEngine;

namespace DefaultNamespace.Extension
{
    public sealed class CustomCoroutine : MonoBehaviour
    {
        private static CustomCoroutine _instance
        {
            get
            {
                if (s_instance == null)
                {
                    GameObject gameObject = new GameObject("[COROUTINE MANAGER]");
                    s_instance = gameObject.AddComponent<CustomCoroutine>();
                    DontDestroyOnLoad(gameObject);
                }

                return s_instance;
            }
        }

        private static CustomCoroutine s_instance;

        public static Coroutine Start(IEnumerator enumerator) => 
            _instance.StartCoroutine(enumerator);

        public static void Stop(Coroutine coroutine) => 
            _instance.StopCoroutine(coroutine);
    }
}