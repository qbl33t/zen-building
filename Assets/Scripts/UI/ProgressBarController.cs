using System;
using Data.Structure;
using MoreMountains.Tools;
using UnityEngine;

namespace UI {
	public class ProgressBarController : MonoBehaviour {
		// public virtual void UpdateBar(float currentValue,float minValue,float maxValue) 

		[SerializeField] private GameObject progressBar;

		private MMProgressBar _progressBar;

		private void Awake() {
			_progressBar = GetComponent<MMProgressBar>();
		}

		public void OnEventGameState(Component sender, object data) {
			if (data is GameState gameState) {
				// Debug.Log("[UI] UpdateProgressBar: received Event [NewCubeSpawned]");
				_progressBar.UpdateBar(gameState.ReachedHeight, 0, gameState.MaxHeight);
			}
		}
	}
}