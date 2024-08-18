using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Collisions;
using Data.Structure;
using EventLogic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;
using Utils;
using Random = System.Random;

namespace Logic {
	public class GhostCubeMachine : MonoBehaviour {
		[SerializeField] private GameObject prefabCube;
		[SerializeField] private GameObject prefabGhostCube;

		[Header("Events 2 Send")]
		[SerializeField] private GameEvent eventNewCubeSpawned;

		private GameState _gameState;
		private readonly List<CubeCollision> _childObjects = new List<CubeCollision>();
		private static Random _rnd = new();
		private CubeCollision _newCube;
		private GameObject _ghostCube;

		//
		// Unity methods & events
		//
		private void Awake() {
			// get all objects for collision
			foreach (Transform children in transform) {
				_childObjects.Add(children.GameObject().GetComponent<CubeCollision>());
			}
		}

		//
		// Received Events
		//
		public void OnEventSpawnCube(Component sender, object data) {
			// Debug.Log("CubeDetectorLogic::OnEventNewCubeSpawn");

			if (_ghostCube != null) {
				Destroy(_ghostCube);
			}

			// Time to spawn some cube - shall we?
			var newCube = Instantiate(prefabCube, _newCube.transform.position, Quaternion.identity);

			// move to new cube
			transform.position = newCube.transform.position;

			// raise event to notify others
			eventNewCubeSpawned.EmitEvent(this, newCube);
		}

		public void OnEventGameRestart(Component sender, object data) {
			// Logg.Me($"game state data: {data}");
			if (data is GameState state) {
				ProceedRestartGame(state);
			}
		}

		//
		// Coroutines
		// 
		IEnumerator SpawningGhostCubes(GameState state) {
			while (true) {
				// iterate over all children and check their status
				var ghostCubePositions = _childObjects.Where(child => !child.IsColliding).ToList();

				if (ghostCubePositions.Count > 0) {
					int randomIndex = _rnd.Next(ghostCubePositions.Count);

					// Get the randomly selected CubeCollision object
					_newCube = ghostCubePositions[randomIndex];

					if (_ghostCube != null) {
						Destroy(_ghostCube);
					}

					// init new cube position
					_ghostCube = Instantiate(prefabGhostCube, _newCube.transform.position, Quaternion.identity);
				}

				yield return new WaitForSeconds(state.Speed);
			}
		}

		//
		// Private helpers
		//
		private void ProceedRestartGame(GameState gameState) {
			// Debug.Log("[GhostCubeMachine] Restarting scene");
			_gameState = gameState;
			// move to starting cube
			transform.position = _gameState.FirstCube.transform.position;

			StopAllCoroutines();
			// adjust to new game state
			StartCoroutine(SpawningGhostCubes(_gameState));
		}
	}
}