using System.Collections.Generic;
using System.Linq;
using Collisions;
using Unity.VisualScripting;
using UnityEngine;
using Random = System.Random;

namespace Logic {
	public class CubeDetectorLogic : MonoBehaviour {
		[SerializeField] public GameObject prefabCube;

		private readonly List<CubeCollision> _childObjects = new List<CubeCollision>();
		private static Random _rnd = new Random();
		private CubeCollision _newCube;

		private void OnEnable() {
			// get all objects for collision
			foreach (Transform children in transform) {
				_childObjects.Add(children.GameObject().GetComponent<CubeCollision>());
			}
		}

		// Start is called once before the first execution of Update after the MonoBehaviour is created
		private void Start() {
			// raise the event
		}

		// Update is called once per frame
		private void Update() {
			// iterate over all children and check their status
			var ghostCubePositions = _childObjects.Where(child => !child.IsColliding).ToList();

			if (ghostCubePositions.Count > 0) {
				int randomIndex = _rnd.Next(ghostCubePositions.Count);

				// Get the randomly selected CubeCollision object
				_newCube = ghostCubePositions[randomIndex];
			}
		}

		public void OnGameEventRaised(Component sender, object data) {
			if (data is string) {
				Debug.Log("CubeDetector received the event: " + data);

				// Time to spawn some cube - shall we?
				var newCube = Instantiate(prefabCube, _newCube.transform.position, Quaternion.identity);

				// move above the spawned cube
				transform.position = newCube.transform.position + Vector3.up * .5f;
			}
		}
	}
}