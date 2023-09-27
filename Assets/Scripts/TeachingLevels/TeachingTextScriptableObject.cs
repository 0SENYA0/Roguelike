using UnityEngine;

namespace Assets.TeachingLevels
{
    [CreateAssetMenu(fileName = "TeachingTextScriptableObject", menuName = "ScriptableObject/TeachingText", order = 0)]
    public class TeachingTextScriptableObject : ScriptableObject
    {
        [SerializeField] [TextArea] private string _textRU;
        [SerializeField] [TextArea] private string _textEN;
        [SerializeField] [TextArea] private string _textTR;
        
        public string TextRU => _textRU;

        public string TextEN => _textEN;

        public string TextTR => _textTR;
    }
}