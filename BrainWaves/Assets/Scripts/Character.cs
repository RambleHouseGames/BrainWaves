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

	public abstract RoomType GetRoomType ();

	protected void InitPosition() {
		TileBase destinationTile = myCollumn.GetCurrentRoom().GetTile(coord);
		if (destinationTile.GetTileType () == TileType.EMPTY) {
			transform.position = destinationTile.transform.position;
		}
	}
	public void resetRoom(){
		coord.x = 4; coord.y = 0;
		TileBase destinationTile = myCollumn.GetCurrentRoom().GetTile(coord);
		transform.position = destinationTile.transform.position;
		transform.SetParent (destinationTile.transform);
	}

	// Get the destination for a move.
	virtual protected Vector2 GetDestination (Move myMove)	{
		return MoveBy (coord, myMove, 1f);
	}

	// Translate the last person's move into my move.
	abstract protected Move InterpretMove (Move yourMove);

	// Send my move to the next person.
	protected bool SendMove(Move myMove) {
		var roomCol = GameData.Instance.GetNextRoomCollumn (myType);
		if (roomCol == null || roomCol.character == null)
			return true;
		return roomCol.character.TryMove (myMove);
	}

	// Receive a move from the last person (or user input) and execute it.
	protected bool TryMove(Move yourMove) {
		Move myMove = InterpretMove (yourMove);

		if (myMove == Move.NONE)
			return true;

		Vector2 destination = GetDestination (myMove);

		Room room = myCollumn.GetCurrentRoom ();
		TileBase destinationTile = room.GetTile(destination);
		TileBase leavingTile = room.GetTile (coord);
		if (destinationTile == null)
			return SendMove (myMove);
		
		TileType tileType = destinationTile.GetTileType ();
		TileType leavingTileType = leavingTile.GetTileType ();

		if (tileType == TileType.DEATH) {
			GameData.Instance.onDeath(false); return false;
		}
		if (!SendMove (myMove))
			return false;

		// Blocking tiles.
		if (tileType == TileType.WALL
			|| tileType == TileType.DOOR)
			return true;

		// Special blocking tile.
		if (tileType == TileType.LEVER) {
			if (myMove == Move.UP) {
				Debug.Log ("Trigger Lever");
				GameData.Instance.RegisterStateChange((destinationTile as LeverTile).Trigger);
			} return true;
		}

		// Push rock?
		Rock rock = room.GetRock (destination);
		if (rock != null) {
			//Debug.Log ("found a rock!");
			bool canPush = TryPushRock (rock, room, destination, myMove);
			if (!canPush)
				return true;
		}

		// Move player.
		transform.position = destinationTile.transform.position;
		transform.SetParent (destinationTile.transform);
		coord = destination;

		// Buttons
		if (tileType == TileType.BUTTON) {
			GameData.Instance.RegisterStateChange((destinationTile as ButtonTile).PlayerOn);
		}
		if (leavingTileType == TileType.BUTTON) {
			GameData.Instance.RegisterStateChange((leavingTile as ButtonTile).PlayerOff);
		}

		// Check For Victory Door
		if (destination == new Vector2 (4, 20)) {
			GameData.Instance.ReportExitDoor (GetRoomType ());
		} return true;
	}

	// Returns all non-null tiles adjacent to the given position.
	public List<TileBase> GetAdjacent(Vector2 position) {
		var result = new List<TileBase> () {
			myCollumn.GetCurrentRoom ().GetTile (position + Vector2.up),
			myCollumn.GetCurrentRoom ().GetTile (position + Vector2.right),
			myCollumn.GetCurrentRoom ().GetTile (position + Vector2.left),
			myCollumn.GetCurrentRoom ().GetTile (position + Vector2.down)
		};
		result.RemoveAll(t => t == null);
		return result;
	}

	public bool TryPushRock(Rock rock, Room room, Vector2 destination, Move move) {
		Vector3 pushTo = MoveBy (destination, move, 1f);
		TileBase pushToTile = room.GetTile (pushTo);
		if (pushToTile == null)
			return false;

		TileType tileType = pushToTile.GetTileType ();
		if (tileType == TileType.WALL
		    || tileType == TileType.LEVER
		    || tileType == TileType.DEATH)
			return false;

		if (room.GetRock (pushTo) != null)
			return false;

		TileBase pushFromTile = room.GetTile (destination);
		rock.Push (room, pushToTile, pushFromTile);
		return true;
	}

	public static Vector2 MoveBy(Vector2 initial, Move moveDirection, float tiles) {
		Vector2 dest;
		switch (moveDirection) {
		case Move.UP:
			dest = initial + tiles * Vector2.up;
			break;
		case Move.DOWN:
			dest = initial + tiles * Vector2.down;
			break;
		case Move.LEFT:
			dest = initial + tiles * Vector2.left;
			break;
		case Move.RIGHT:
			dest = initial + tiles * Vector2.right;
			break;
		case Move.NONE:
			dest = initial;
			break;
		default:
			throw new System.ArgumentOutOfRangeException ();
		}
		return dest;
	}
}
