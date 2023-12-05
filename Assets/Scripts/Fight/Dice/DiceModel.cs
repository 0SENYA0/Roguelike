using System.Collections.Generic;
using UnityEngine;

namespace Assets.Fight.Dice
{
    public class DiceModel
    {
        public DiceModel(IReadOnlyList<Sprite> sprites) =>
            Sprites = sprites;

        public IReadOnlyList<Sprite> Sprites { get; }
    }
}