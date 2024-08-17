using System;
using Data.Structure;
using TMPro;
using TMPro.Examples;
using UnityEngine;

namespace UI {
	public class StatisticsController : MonoBehaviour {
		private TextMeshProUGUI _textMesh;
		private const string _header = "Statistics\n";

		private void Awake() {
			_textMesh = GetComponent<TextMeshProUGUI>();
		}

		public void OnEventGameState(Component component, object data) {
			// TODO: this if should be removed and changed  
			if (data is GameState state) {
				_textMesh.text = _header;
				_textMesh.text +=
					$"MaxHeight: {state.MaxHeight}\n Height: {state.ReachedHeight}\n Speed: {state.Speed}\n Mass: {state.CubeMass} ";
			}
		}
	}
}