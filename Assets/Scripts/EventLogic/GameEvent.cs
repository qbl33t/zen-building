using System.Collections.Generic;
using UnityEngine;

namespace EventLogic {
	[CreateAssetMenu(menuName = "Game Events/GameEvent")]
	public class GameEvent : ScriptableObject {
		private readonly HashSet<GameEventListener> _listeners = new();

		public void EmitEvent(Component sender, object data) {
			foreach (GameEventListener eventListener in _listeners) {
				eventListener.OnEventEmitted(sender, data);
			}
		}

		public void RegisterListener(GameEventListener listener) {
			_listeners.Add(listener);
		}

		public void UnregisterListener(GameEventListener listener) {
			_listeners.Remove(listener);
		}
	}
}