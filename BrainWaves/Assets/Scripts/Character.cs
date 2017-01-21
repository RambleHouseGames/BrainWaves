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
	virtual protected Vector2 GetDestination (Move myMove)	{
		Vector2 dest;
		switch (myMove) {
		case Move.UP:
			dest = coord + new Vector2 (0, 1);
			break;
		case Move.DOWN:
			dest = coord + new Vector2 (0, -1);
			break;
		case Move.LEFT:
			dest = coord + new Vector2 (-1, 0);
			break;
		case Move.RIGHT:
			dest = coord + new Vector2 (1, 0);
			break;
		default:
			throw new System.ArgumentOutOfRangeException ();
		}
		return dest;
	}

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

		if (myMove == Move.NONE)
			return;

		Vector2 destination = GetDestination (myMove);
		TileBase destinationTile = myCollumn.GetCurrentRoom().GetTile(destination);
		if (destinationTile == null)
			return;
		
		TileType tileType = destinationTile.GetTileType ();

		if (tileType != TileType.EMPTY)
			Debug.Log (tileType);

		// Blocking tiles.
		if (tileType == TileType.WALL
			|| tileType == TileType.LEVER
			|| tileType == TileType.DOOR)
			return;

		// if tile contains boulder
		// -if can push it
		// --push it
		// -if can't push it
		// --cancel move

		transform.position = destinationTile.transform.position;
		transform.SetParent (destinationTile.transform);
		coord = destination;

		// Special tiles.
		if (tileType == TileType.BUTTON) {
			// TODO: trigger button
		} else if (tileType == TileType.DEATH) {
			// TODO: trigger death
		} else {
			// if any *adjacent* tile is a lever, trigger lever
		}
	}
}
