using UnityEngine;

namespace Assets.UI
{
	public class FollowToTarget : MonoBehaviour
	{
		[SerializeField] private Transform _target;
		[SerializeField] [Range(0, 10f)] private float _velocity = 1.5f;
		[SerializeField] private float _offset = 10f;

		private void Awake()
		{
			transform.position = new Vector3()
			{
				x = _target.position.x,
				y = _target.position.y,
				z = _target.position.z - _offset,
			};
		}

		private void Update()
		{
			Vector3 target = new Vector3()
			{
				x = _target.position.x,
				y = _target.position.y,
				z = _target.position.z - _offset,
			};

			Vector3 pos = Vector3.Lerp(transform.position, target, _velocity * Time.deltaTime);

			transform.position = pos;
		}
	}
}