using EventLogic;
using UnityEngine;
using UnityEngine.UIElements;

public class RaiseGameEventOnKeydown : MonoBehaviour {
	[Header("Events")]
	public GameEvent gameEvent;

	// Update is called once per frame
	private void Update() {
		if (Input.GetKeyDown(KeyCode.Space)) {
			Debug.Log("Space was pressed!");
			gameEvent.Raise(this, "Just letting you know, that Space was pressed!");
		}
	}
}