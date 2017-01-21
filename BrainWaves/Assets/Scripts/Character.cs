using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Character : MonoBehaviour {

	[SerializeField]
	protected RoomType myType;

	[SerializeField]
	protected Vector2 coord;

	[SerializeField]
	protected RoomCollumn myCollumn;

	protected void InitPosition() {
		TileBase destinationTile = myCollumn.GetCurrentRoom().GetTile(coord);
		if (destinationTile.GetTileType () == TileType.EMPTY) {
			transform.position = destinationTile.transform.position;
		}
	}

	// Get the destination for a move.
	abstract protected Vector2 GetDestination (Move myMove);

	// Translate the last person's move into my move.
	abstract protected Move InterpretMove (Move yourMove);

	// Send my move to the next person.
	protected void SendMove(Move myMove) {
		var roomCol = GameData.Instance.GetNextRoomCollumn (myType);
		if (roomCol == null || roomCol.character == null)
			return;
		roomCol.character.TryMove (myMove);
	}

	// Receive a move from the last person (or user input) and execute it.
	protected void TryMove(Move yourMove) {
		Move myMove = InterpretMove (yourMove);
		SendMove (myMove);

		Vector2 destination = GetDestination (myMove);
		TileBase destinationTile = myCollumn.GetCurrentRoom().GetTile(destination);
		if (destinationTile != null && destinationTile.GetTileType () == TileType.EMPTY) {
			transform.position = destinationTile.transform.position;
			transform.SetParent (destinationTile.transform);
			coord = destination;
		}
	}
}
