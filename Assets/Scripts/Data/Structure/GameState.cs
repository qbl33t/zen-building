using UnityEngine;

namespace Data.Structure {
	public struct GameState {
		// Game definition of the level
		public int CurrentLevel { get; set; }

		// Current max level (height)
		public int MaxHeight { get; set; }

		// Game definition
		public float Speed { get; set; }
		public float CubeMass { get; set; }

		// Current player reached height
		public int ReachedHeight { get; set; }

		public GameObject FirstCube { get; set; }

		public GameState(int currentLevel, int maxHeight, float speed, float cubeMass, int reachedHeight) {
			CurrentLevel = currentLevel;
			MaxHeight = maxHeight;
			Speed = speed;
			CubeMass = cubeMass;
			ReachedHeight = reachedHeight;
			FirstCube = null;
		}
	}
}