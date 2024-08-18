using System;
using Data.Structure;
using TMPro;
using TMPro.Examples;
using UnityEngine;
using Utils;

namespace UI {
	public class StatisticsController : MonoBehaviour {
		private TextMeshProUGUI _textMesh;
		private const string _header = "Statistics:\n";

		private void Awake() {
			_textMesh = GetComponent<TextMeshProUGUI>();
		}

		public void OnEventGameStart(Component component, object data) {
			Logg.Me($"received state: {data}");
			if (data is GameState state) {
				UpdateText("Start", state);
			}
		}

		public void OnEventGameRestart(Component component, object data) {
			Logg.Me($"received state: {data}");
			if (data is GameState state) {
				UpdateText("Restart | Next Level", state);
			}
		}

		public void OnEventGameUpdate(Component component, object data) {
			// TODO: this if should be removed and changed  
			Logg.Me($"received state: {data}");
			if (data is GameState state) {
				UpdateText("Update", state);
			}
		}

		//
		// Private helpers
		//
		private void UpdateText(string prefix, GameState state) {
			_textMesh.text = $"{_header}: {prefix}\n";
			_textMesh.text +=
				$"MaxHeight: {state.MaxHeight}\n Height: {state.ReachedHeight}\n Speed: {state.Speed}\n Mass: {state.CubeMass} ";
		}
	}
}