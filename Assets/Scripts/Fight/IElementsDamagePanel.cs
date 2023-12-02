using UnityEngine.UI;

namespace Assets.Fight
{
	public interface IElementsDamagePanel
	{
		void ShowPanel();

		public void HidePanel();

		public Button Exit { get; }

		public IElementInfoLine FireElementInfoLine { get; }
		
		public IElementInfoLine TreeElementInfoLine { get; }

		public IElementInfoLine WaterElementInfoLine { get; }

		public IElementInfoLine MetalElementInfoLine { get; }

		public IElementInfoLine StoneElementInfoLine { get; }
	}
}