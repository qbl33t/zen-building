using System;
using System.Collections.Generic;
using System.Linq;
using Data.Structure;
using EventLogic;
using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.Serialization;

namespace GameEngine {
	public class GameEngine : MonoBehaviour {
		[SerializeField] private CinemachineCamera cinemachineCamera;

		// [Header("Logic")]
		// [SerializeField] public GameObject cubeSpawner;

		[Header("Events")]
		public GameEvent keyDownSpace;

		public GameEvent reachedHeight;

		private readonly List<MeshFilter> _cubeSpawnedMeshFilter = new();
		private ReachedHeight _reachedHeight;

		private void OnEnable() {
			// TODO: this should be then moved to proper GameState structure
			_reachedHeight.Height = 1;
			_reachedHeight.MaxHeight = 10;
		}

		private void Start() {
			// TODO: leave it here or take it to another function based on "GameState" ?
			// like GameStart, GameRestart, Game* ?
			reachedHeight.EmitEvent(this, _reachedHeight);
		}

		private void Update() {
			if (Input.GetKeyDown(KeyCode.Space)) {
				keyDownSpace.EmitEvent(this, null);
			}
		}

		//
		// Events
		//
		public void OnEventNewCubeSpawn(Component sender, object data) {
			Debug.Log("[GameEngine][OnNewCubeSpawned]: " + data);

			if (data is GameObject newCubeGameObject) {
				var newTransform = newCubeGameObject.transform;

				_cubeSpawnedMeshFilter.Add(newCubeGameObject.GetComponent<MeshFilter>());

				cinemachineCamera.Follow = newTransform;
				cinemachineCamera.LookAt = newTransform;

				CalculateHeight();
			}
		}

		//
		// Private logic
		//
		private void CalculateHeight() {
			float highestY = _cubeSpawnedMeshFilter
				.SelectMany(mf => mf.mesh.vertices.Select(v => mf.transform.TransformPoint(v).y))
				.Max();

			if (_reachedHeight.Height < highestY) {
				// emit event
				_reachedHeight.Height = (int)highestY;
				reachedHeight.EmitEvent(this, _reachedHeight);
			}
		}
	}
}