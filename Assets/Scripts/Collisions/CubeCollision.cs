using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using EventLogic;
using UnityEngine;
using UnityEngine.Serialization;
using Utils;

namespace Collisions {
	public class CubeCollision : MonoBehaviour {
		[SerializeField] public Material materialCollision;

		[Header("Events 2 Send")]
		[SerializeField] public GameEvent eventTowerFellDown;

		private bool _isColliding;
		private Material _material;
		private MeshRenderer _meshRender;
		private GameObject _cube;

		private void Awake() {
			_material = GetComponent<Renderer>().materials[0];
			_meshRender = GetComponent<MeshRenderer>();
			_cube = GetComponent<GameObject>();
		}

		private void Update() {
			if (Input.GetKeyDown(KeyCode.R)) {
				Logg.Me("Collision cube update -> Tower Fell Down");
				IsColliding = false;
				eventTowerFellDown.EmitEvent(this, _cube);
			}
		}

		//
		// Events - Game
		//
		public void OnEventNewCubeSpawned(Component sender, object data) {
			IsColliding = false;
		}

		//
		// Events - Unity
		//

		private void OnTriggerEnter(Collider other) {
			if (other.CompareTag("Cube")) {
				// Debug.Log("Collision ENTER");
				IsColliding = true;

				_meshRender.material = materialCollision;
			}
		}

		private void OnTriggerStay(Collider other) {
			if (other.CompareTag("Cube")) {
				IsColliding = true;

				_meshRender.material = materialCollision;
			}
		}

		private void OnTriggerExit(Collider other) {
			if (other.CompareTag("Cube")) {
				// Debug.Log("Collision EXIT");
				IsColliding = false;

				_meshRender.material = _material;
			}
		}

		//
		// Private helper
		//
		public bool IsColliding {
			get => _isColliding;
			private set {
				_isColliding = value;
				_meshRender.material = value ? materialCollision : _material;
			}
		}
	}
}