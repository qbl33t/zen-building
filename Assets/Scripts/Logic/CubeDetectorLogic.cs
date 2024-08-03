using System;
using System.Collections.Generic;
using EventLogic;
using Unity.VisualScripting;
using UnityEngine;

namespace Logic {
	public class CubeDetectorLogic : MonoBehaviour {
		private List<GameObject> _childObjects = new List<GameObject>();

		private void OnEnable() {
			// get all objects for collision
			foreach (Transform children in transform) {
				_childObjects.Add(children.GameObject());
			}
		}

		// Start is called once before the first execution of Update after the MonoBehaviour is created
		private void Start() {
			// raise the event
		}

		// Update is called once per frame
		private void Update() {
			// iterate over all children and check their status
		}

		public void OnGameEventRaised(Component sender, object data) {
			if (data is string) {
				Debug.Log("CubeDetector received the event: " + data);
			}
		}
	}
}