using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LazyCharacter : Character {

	private bool lazyThisTurn = false;

	void Awake()
	{
		InitPosition ();
	}

	void Start()
	{
		GameData.Instance.AddProgressCallback (onProgressChanged);
	}

	public override RoomType GetRoomType ()
	{
		return RoomType.LAZY;
	}

	override protected Move InterpretMove (Move yourMove) {
		Move move;
		if (lazyThisTurn)
			move = Move.NONE;
		else
			move = yourMove;
		lazyThisTurn = !lazyThisTurn;
		return move;
	}

	private void onProgressChanged()
	{
		Room newRoom = myCollumn.GetCurrentRoom ();
		TileBase tile = newRoom.GetTile (new Vector2(4, 0));
		transform.position = tile.transform.position;
		transform.SetParent (tile.transform);
		coord = new Vector2 (4, 0);
	}
}
