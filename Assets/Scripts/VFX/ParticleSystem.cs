using UnityEngine;

namespace VFX {
	public class ParticleSystem : MonoBehaviour {
		[SerializeField] private GameObject prefabNewBlockAdded;

		public void OnNewBlockAdded(Component sender, object data) {
			if (data is Transform trans) {
				var position = trans.position;
				var particle = Instantiate(prefabNewBlockAdded, position, Quaternion.identity);
			}
		}
	}
}