using Assets.Fight.Element;
using Assets.Infrastructure;
using UnityEngine;

namespace Assets.ScriptableObjects
{
    [CreateAssetMenu(fileName = "NameOfElements", menuName = "ScriptableObject/NameOfElements", order = 0)]
    public class NameOfElementsScriptableObject : ScriptableObject
    {
        [SerializeField] private LocalizedText _fire;
        [SerializeField] private LocalizedText _tree;
        [SerializeField] private LocalizedText _water;
        [SerializeField] private LocalizedText _metal;
        [SerializeField] private LocalizedText _stone;

        public string GetElementName(Element element)
        {
            var lang = Game.GameSettings.CurrentLocalization;
            
            switch (element)
            {
                case Element.Fire:
                    return _fire.GetLocalization(lang);
                case Element.Tree:
                    return _tree.GetLocalization(lang);
                case Element.Water:
                    return _water.GetLocalization(lang);
                case Element.Metal:
                    return _metal.GetLocalization(lang);
                case Element.Stone:
                    return _stone.GetLocalization(lang);
                default:
                    return _fire.GetLocalization(lang);
            }
        }
    }
}