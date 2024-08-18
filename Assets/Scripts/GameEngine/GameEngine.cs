using System;
using System.Collections.Generic;
using System.Linq;
using Data.Structure;
using EventLogic;
using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.Serialization;
using Utils;
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
			}
			// else if (Input.GetKeyDown(KeyCode.R)) {
			// 	ProceedRestartLevel();
			// }
		}

		private void ProceedNextLevel() {
			// calculate GameState for next level
			_gameState = GameStateLogic.NextGameState(_gameState);

			ResetScene();

			Logg.Me($"new state: {_gameState}");
			eventGameNextLevel.EmitEvent(this, _gameState);
		}

		private void ProceedRestartLevel() {
			// reset GameState -> just by changing reached height
			ResetScene();

			Logg.Me($"new state: {_gameState}");
			eventGameRestart.EmitEvent(this, _gameState);
		}

		private void ResetScene() {
			// TODO: init transition to "new state" of the game 

			_gameState.ReachedHeight = 1;

			// 1. destroy all cubes + spawn first one (this could stay)
			_cubeTower.ForEach(cube => cube.Destroy());
			_cubeTower.Clear();

			// move to first cube
			MoveCameraAt(cubeStart);
		}

		//
		// Events
		//
		public void OnEventTowerFellDown(Component sender, object data) {
			if (data is GameObject gameObject) {
				// TODO: now gameObject isn't needed but maybe in the future some nice effect/slow mo? :)
				// or just particle system!
				ProceedRestartLevel();
			}
		}

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
					Logg.Me($"new state: {_gameState}");
					eventGameUpdate.EmitEvent(this, _gameState);
				}
			}
		}
	}
}