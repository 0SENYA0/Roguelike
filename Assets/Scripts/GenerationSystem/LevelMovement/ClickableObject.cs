using UnityEngine;

namespace Assets.Scripts.GenerationSystem.LevelMovement
{
    public class ClickableObject : MonoBehaviour
    {
        void OnMouseDown(){
            Debug.Log("Sprite Clicked");
        }
    }
}