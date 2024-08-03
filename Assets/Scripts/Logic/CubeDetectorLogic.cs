using System;
using EventLogic;
using UnityEngine;

namespace Logic {
	public class CubeDetectorLogic : MonoBehaviour {
		// Start is called once before the first execution of Update after the MonoBehaviour is created
		private void Start() {
			// raise the event
		}

		// Update is called once per frame
		private void Update() {
		}

		public void OnGameEventRaised(Component sender, object data) {
			if (data is string) {
				Debug.Log("CubeDetector received the event: " + data);
			}
		}
	}
}