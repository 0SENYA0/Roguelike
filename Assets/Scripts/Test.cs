using UnityEngine;

namespace DefaultNamespace
{
    public class Test : MonoBehaviour
    {
        private void Start()
        {
            int count = 1000;

            for (int j = 0; j < 10; j++)
            {
                int sum_1 = 0;
                int sum_2 = 0;
            
                for (int i = 0; i < count; i++)
                {
                    int choice_1 = Random.Range(0, 4);
                    int choice_2 = Mathf.FloorToInt(Random.value * 3.99f);
                

                    sum_1 += choice_1;
                    sum_2 += choice_2;
                }

                float midValue_1 = sum_1 / (count * 1f);
                float midValue_2 = sum_2 / (count * 1f);
            
                if (midValue_1 > midValue_2)
                    Debug.Log($"<color=YELLOW>{midValue_1}</color>\t<color=ORANGE>{midValue_2}</color>");
                else
                    Debug.Log($"<color=ORANGE>{midValue_1}</color>\t<color=YELLOW>{midValue_2}</color>");
            }
        }
    }
}