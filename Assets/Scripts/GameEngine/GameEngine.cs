using System;
using EventLogic;
using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.Serialization;

namespace GameEngine {
	public class GameEngine : MonoBehaviour {
		[SerializeField] private CinemachineCamera cinemachineCamera;

		[Header("Logic")]
		[SerializeField] public GameObject cubeSpawner;

		[Header("Raise Events")]
		public GameEvent keyDownSpace;

		private void OnEnable() {
		}

		private void Start() {
		}

		private void Update() {
			if (Input.GetKeyDown(KeyCode.Space)) {
				Debug.Log("Space was pressed!");
				keyDownSpace.Raise(this, "Just letting you know, that Space was pressed!");
			}
		}

		//
		// Listen Events
		//
		public void OnNewCubeSpawned(Component sender, object data) {
			Debug.Log("[GameEngine][OnNewCubeSpawned]: " + data);
			if (data is Transform newTarget) {
				cinemachineCamera.Follow = newTarget;
				cinemachineCamera.LookAt = newTarget;
			}
		}
	}
}