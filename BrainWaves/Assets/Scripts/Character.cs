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

	protected void onProgressChanged()
	{
		Room newRoom = myCollumn.GetCurrentRoom ();
		TileBase tile = newRoom.GetTile (new Vector2(4, 0));
		transform.position = tile.transform.position;
		transform.SetParent (tile.transform);
		coord = new Vector2 (4, 0);
	}

	// Translate the last person's move into my move.
	virtual protected Move InterpretMove (Move yourMove) {
		return yourMove;
	}

	// Send my move to the next person.
	protected bool SendMove(Move myMove, int tiles) {
		var roomCol = GameData.Instance.GetNextRoomCollumn (myType);
		if (roomCol == null || roomCol.character == null)
			return true;
		return roomCol.character.TryMove (myMove, tiles);
	}

	virtual protected void FindLegalMove(Move myMove, out TileBase entering, out TileBase bumpingInto) {
		entering = null;
		bumpingInto = null;

		var tile = myCollumn.GetCurrentRoom ().GetTile (MoveBy (coord, myMove, 1));

		if (tile == null) {
			return;
		} else if (tile.GetTileType ().IsWalkable ()) {
			entering = tile;
		} else {
			bumpingInto = tile;
		}
	}

	// Receive a move from the last person (or user input) and execute it.
	virtual protected bool TryMove(Move yourMove, int tiles) {
		//Debug.Log ("Trying move " + myType + " " + tiles);

		Move myMove = InterpretMove (yourMove);

		if (myMove == Move.NONE)
			return true;

		TileBase entering;
		TileBase bumpingInto;
		FindLegalMove (myMove, out entering, out bumpingInto);

		TileBase leaving = (entering != null) ? myCollumn.GetCurrentRoom().GetTile(coord) : null;

		// Quit if we've died.
		if (TileType.DEATH.TypeEquals(entering)) {
			//GameData.Instance.onDeath(false);
			return false;
		}

		// Not dead: send moves, but quit if anyone else dies.
		if (SendMove (myMove, 1) == false) {
			return false;
		}

		// Special blocking tile.
		if (TileType.LEVER.TypeEquals(bumpingInto) && myMove == Move.UP) {
			Debug.Log ("Trigger Lever");
			GameData.Instance.RegisterStateChange((bumpingInto as LeverTile).Trigger);
			return true;
		}

		if (entering != null) {
			// Push rock?
			Rock rock = myCollumn.GetCurrentRoom ().GetRock (entering);
			if (rock != null) {
				//Debug.Log ("found a rock!");
				bool canPush = TryPushRock (rock, myCollumn.GetCurrentRoom(), entering, myMove);
				if (!canPush)
					return true;
			}

			// Move player.
			transform.position = entering.transform.position;
			transform.SetParent (entering.transform);
			coord = myCollumn.GetCurrentRoom().GetCoord(entering);
		}

		// Buttons
		if (TileType.BUTTON.TypeEquals(entering)) {
			GameData.Instance.RegisterStateChange((entering as ButtonTile).PlayerOn);
		}
		if (TileType.BUTTON.TypeEquals(leaving)) {
			GameData.Instance.RegisterStateChange((leaving as ButtonTile).PlayerOff);
		}

		// Check For Victory Door
		if (isVictory()) {
			GameData.Instance.checkVictory();
		}

		return true;
	}

	public bool isVictory(){
		return coord == new Vector2 (4, 20);
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

	public bool TryPushRock(Rock rock, Room room, TileBase pushFromTile, Move move) {
		Vector2 pushFrom = room.GetCoord (pushFromTile);
		Vector2 pushTo = MoveBy (pushFrom, move, 1f);
		TileBase pushToTile = room.GetTile (pushTo);

		if (pushToTile == null)
			return false;

		TileType tileType = pushToTile.GetTileType ();
		if (tileType == TileType.WALL
			|| tileType == TileType.DOOR
		    || tileType == TileType.LEVER
		    || tileType == TileType.DEATH)
			return false;

		// Can't push if there's another rock there.
		if (room.GetRock (pushToTile) != null)
			return false;

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
