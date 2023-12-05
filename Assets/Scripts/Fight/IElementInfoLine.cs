using UnityEngine.UI;

namespace Assets.Fight
{
	public interface IElementInfoLine
	{
		public Image Element { get; }

		public ILineInfo InfoInLine { get; }
	}
}