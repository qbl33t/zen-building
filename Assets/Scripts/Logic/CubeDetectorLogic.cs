using System.Collections.Generic;
using System.Linq;
using Collisions;
using EventLogic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;
using Random = System.Random;

namespace Logic {
	public class CubeDetectorLogic : MonoBehaviour {
		[SerializeField] private GameObject prefabCube;
		[SerializeField] private GameObject prefabGhostCube;
		[SerializeField] private GameEvent newCubeSpawned;

		[SerializeField] private float speedSpawn = 0.5f;

		private readonly List<CubeCollision> _childObjects = new List<CubeCollision>();
		private static Random _rnd = new Random();
		private CubeCollision _newCube;
		private GameObject _ghostCube;

		private void OnEnable() {
			// get all objects for collision
			foreach (Transform children in transform) {
				_childObjects.Add(children.GameObject().GetComponent<CubeCollision>());
			}
		}

		// Start is called once before the first execution of Update after the MonoBehaviour is created
		private void Start() {
			InvokeRepeating(nameof(SpawningGhostBlock), 0, speedSpawn);
		}

		// Update is called once per frame
		private void Update() {
		}

		//
		// Coroutines
		// 
		private void SpawningGhostBlock() {
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
		}


		//
		// Events
		//
		public void OnGameEventRaised(Component sender, object data) {
			if (data is string) {
				Debug.Log("CubeDetector received the event: " + data);

				// Time to spawn some cube - shall we?
				var newCube = Instantiate(prefabCube, _newCube.transform.position, Quaternion.identity);

				// move above the spawned cube
				transform.position = newCube.transform.position - Vector3.down * 0.5f;

				// raise event to notify others
				newCubeSpawned.Raise(this, _ghostCube);
			}
		}
	}
}