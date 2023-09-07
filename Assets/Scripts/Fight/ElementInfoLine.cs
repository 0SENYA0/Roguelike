using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Fight
{
    public class ElementInfoLine : MonoBehaviour, IElementInfoLine
    {
        [field: SerializeField] public Element.Element Element { get; set; }

        [SerializeField] private LineInfo _lineInfo;

        public ILineInfo InfoInLine => _lineInfo;

        [Serializable]
        class LineInfo : ILineInfo
        {
            [field: SerializeField] public TMP_Text Damage { get; set; }
            [field: SerializeField] public TMP_Text ChanceToSplash { get; set; }
            [field: SerializeField] public TMP_Text ChanceCriticalDamage { get; set; }
            [field: SerializeField] public TMP_Text ValueModifier { get; set; }
            [SerializeField] private Button _buttonAttack;

            public Button ButtonAttack => _buttonAttack;
        }
    }

    public interface ILineInfo
    {
        public TMP_Text Damage { get; set; }

        public TMP_Text ChanceToSplash { get; set; }

        public TMP_Text ChanceCriticalDamage { get; set; }

        public TMP_Text ValueModifier { get; set; }
        public Button ButtonAttack { get; }
    }

    public interface IElementInfoLine
    {
        public Element.Element Element { get; set; }

        public ILineInfo InfoInLine { get; }
    }
}