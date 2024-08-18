using System;
using System.Collections.Generic;
using System.Linq;
using Data.Structure;
using EventLogic;
using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.Serialization;
using VHierarchy.Libs;

namespace GameEngine {
	public class GameEngine : MonoBehaviour {
		[SerializeField] private CinemachineCamera cinemachineCamera;

		[SerializeField] private GameObject cubeStart;
		// [Header("Logic")]
		// [SerializeField] public GameObject cubeSpawner;

		[Header("Events 2 Send")]
		public GameEvent eventSpawnCube;

		public GameEvent eventGameStart;
		public GameEvent eventGameUpdate;
		public GameEvent eventGameRestart;
		public GameEvent eventGameNextLevel;

		// Base init of the first GameState
		private static readonly GameState GAME_STATE_INIT = new(
			currentLevel: 1,
			maxHeight: 3,
			speed: 1f,
			cubeMass: 1f,
			reachedHeight: 1
		);

		// list of all new cubes spawned by player
		private readonly List<GameObject> _cubeTower = new();

		// Current game state of the game
		private GameState _gameState;

		private void Awake() {
			_gameState = GAME_STATE_INIT;
			_gameState.FirstCube = cubeStart;
		}

		private void Start() {
			// Pass current event state
			eventGameStart.EmitEvent(this, _gameState);
		}

		private void Update() {
			if (Input.GetKeyDown(KeyCode.Space)) {
				eventSpawnCube.EmitEvent(this, null);
			} else if (Input.GetKeyDown(KeyCode.R)) {
				ProceedRestartLevel();
			}
		}

		private void ProceedNextLevel() {
			Debug.Log("[GameEngine] ProceedNextLevel");
			// calculate GameState for next level
			_gameState = GameStateLogic.NextGameState(_gameState);

			Debug.Log("[GameEngine] Emitting event GameNextLevel");
			eventGameNextLevel.EmitEvent(this, _gameState);
			ResetScene();
		}

		private void ProceedRestartLevel() {
			// reset game state just by reached height
			_gameState.ReachedHeight = 1;
			eventGameRestart.EmitEvent(this, _gameState);
			ResetScene();
		}

		private void ResetScene() {
			// init transition for the next level
			// 0. play transition between levels

			// 1. destroy all cubes + spawn first one (this could stay)
			_cubeTower.ForEach(cube => cube.Destroy());
			_cubeTower.Clear();

			// move to first cube
			MoveCameraAt(cubeStart);
		}

		//
		// Events
		//
		public void OnEventNewCube(Component sender, object data) {
			if (data is GameObject newCubeGameObject) {
				_cubeTower.Add(newCubeGameObject);

				MoveCameraAt(newCubeGameObject);

				CalculateHeight();
			}
		}

		//
		// Private logic
		//
		private void MoveCameraAt(GameObject cube) {
			var newTransform = cube.transform;

			cinemachineCamera.Follow = newTransform;
			cinemachineCamera.LookAt = newTransform;
		}

		private void CalculateHeight() {
			// it is bad practice?
			float highestY = _cubeTower.Select(cube => cube.GetComponent<MeshFilter>())
				.Where(meshFilter => meshFilter != null)
				.SelectMany(mf => mf.mesh.vertices.Select(v => mf.transform.TransformPoint(v).y))
				.Max();

			if (_gameState.ReachedHeight < highestY) {
				_gameState.ReachedHeight = (int)highestY;

				if (_gameState.ReachedHeight >= _gameState.MaxHeight) {
					// spawn new level
					ProceedNextLevel();
				} else {
					// emit event
					eventGameUpdate.EmitEvent(this, _gameState);
				}
			}
		}
	}
}