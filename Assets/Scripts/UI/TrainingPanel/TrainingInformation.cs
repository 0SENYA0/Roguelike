using System.Collections.Generic;
using UnityEngine;

namespace Assets.UI.TrainingPanel
{
	[CreateAssetMenu(fileName = "TrainingInformation", menuName = "ScriptableObject/TrainingInformation", order = 0)]
	public class TrainingInformation : ScriptableObject
	{
		[SerializeField] private List<BacgroundInfo> _information;

		public List<BacgroundInfo> Information => _information;
	}
}