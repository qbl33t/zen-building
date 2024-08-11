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

		public void OnEventReachedHeight(Component sender, object data) {
			if (data is ReachedHeight reachedHeight) {
				Debug.Log("[UI] UpdateProgressBar: received Event [NewCubeSpawned]");
				_progressBar.UpdateBar(reachedHeight.Height, 0, reachedHeight.MaxHeight);
			}
		}
	}
}