using System;
using MoreMountains.Tools;
using UnityEngine;

namespace UI {
	public class ProgressBarController : MonoBehaviour {
		// public virtual void UpdateBar(float currentValue,float minValue,float maxValue) 

		[SerializeField] private GameObject progressBar;

		private MMProgressBar _progressBar;

		private void Start() {
			_progressBar = GetComponent<MMProgressBar>();
		}

		public void OnNewCubeSpawned(Component sender, object data) {
			Debug.Log("[UI] UpdateProgressBar: received Event [NewCubeSpawned]");
			_progressBar.UpdateBar(1, 1, 10);
		}
	}
}