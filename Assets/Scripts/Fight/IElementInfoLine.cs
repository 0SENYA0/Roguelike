using UnityEngine.UI;

namespace Assets.Fight
{
	public interface IElementInfoLine
	{
		public Image Element { get; set; }

		public ILineInfo InfoInLine { get; }
	}
}