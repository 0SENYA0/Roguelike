using UnityEngine;
using UnityEngine.UI;

namespace Assets.Fight
{
    public class ElementInfoLine : MonoBehaviour, IElementInfoLine
    {
        [SerializeField] private LineInfo _lineInfo;

        [field: SerializeField] public Image Element { get; private set; }

        public ILineInfo InfoInLine => _lineInfo;
    }
}