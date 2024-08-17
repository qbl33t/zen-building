using Data.Structure;

namespace GameEngine {
	public class GameStateLogic {
		public static GameState NextGameState(GameState gameState) {
			// TODO: here should be passed some modifiers for calculation of the next level
			var nextGameState = gameState;
			nextGameState.CurrentLevel += 1;
			nextGameState.MaxHeight += 1;
			nextGameState.Speed -= 0.1f;

			return nextGameState;
		}
	}
}