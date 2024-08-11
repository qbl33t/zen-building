using System;
using UnityEngine;
using UnityEngine.Events;

namespace EventLogic {
	[Serializable]
	public class GenericGameEvent : UnityEvent<Component, object> {
	}

	public class GameEventListener : MonoBehaviour {
		public GameEvent gameEvent;
		public GenericGameEvent response;

		private void OnEnable() {
			gameEvent.RegisterListener(this);
		}

		private void OnDisable() {
			gameEvent.UnregisterListener(this);
		}

		public void OnEventEmitted(Component sender, object data) {
			response.Invoke(sender, data);
		}
	}
}