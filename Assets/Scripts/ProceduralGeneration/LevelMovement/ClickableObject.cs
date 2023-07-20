using UnityEngine;

namespace Assets.Scripts.ProceduralGeneration.LevelMovement
{
    public class ClickableObject : MonoBehaviour
    {
        void OnMouseDown(){
            Debug.Log("Sprite Clicked");
        }
    }
}