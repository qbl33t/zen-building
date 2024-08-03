using System;
using EventLogic;
using UnityEngine;

namespace GameEngine {
	public class GameEngine : MonoBehaviour {
		[Header("Logic")]
		[SerializeField] public GameObject cubeSpawner;

		[Header("Events")]
		public GameEvent gameEvent;

		private void OnEnable() {
		}

		private void Start() {
		}

		private void Update() {
			if (Input.GetKeyDown(KeyCode.Space)) {
				Debug.Log("Space was pressed!");
				gameEvent.Raise(this, "Just letting you know, that Space was pressed!");
			}
		}
	}
}