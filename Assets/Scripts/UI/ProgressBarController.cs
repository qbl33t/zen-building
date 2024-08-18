using System;
using Data.Structure;
using MoreMountains.Tools;
using UnityEngine;

namespace UI {
	public class ProgressBarController : MonoBehaviour {
		[SerializeField] private GameObject progressBar;

		private MMProgressBar _progressBar;

		private void Awake() {
			_progressBar = GetComponent<MMProgressBar>();
			// TODO: somehow init the progress bar "at the start line" without effects?
			// change it in the UI itself?
		}

		public void OnEventGameRestart(Component sender, object data) {
			if (data is GameState gameState) {
				_progressBar.UpdateBar(gameState.ReachedHeight, 0, gameState.MaxHeight);
			}
		}

		public void OnEventGameUpdate(Component sender, object data) {
			if (data is GameState gameState) {
				_progressBar.UpdateBar(gameState.ReachedHeight, 0, gameState.MaxHeight);
			}
		}
	}
}