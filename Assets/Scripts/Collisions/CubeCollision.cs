using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Serialization;

namespace Collisions {
	public class CubeCollision : MonoBehaviour {
		public Material materialCollision;

		private Material _material;
		private MeshRenderer _meshRender;

		private void Start() {
			_material = GetComponent<Renderer>().materials[0];
			_meshRender = GetComponent<MeshRenderer>();
		}

		private void OnTriggerEnter(Collider other) {
			if (other.CompareTag("Cube")) {
				Debug.Log("Collision ENTER");

				_meshRender.material = materialCollision;
			}
		}

		private void OnTriggerExit(Collider other) {
			if (other.CompareTag("Cube")) {
				Debug.Log("Collision EXIT");

				_meshRender.material = _material;
			}
		}
	}
}