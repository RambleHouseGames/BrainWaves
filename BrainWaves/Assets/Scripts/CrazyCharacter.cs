﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrazyCharacter : Character {

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
		return RoomType.CRAZY;
	}

	override protected Move InterpretMove (Move yourMove) {
		return yourMove;
	}

	// Get the destination for a move.
	override protected Vector2 GetDestination (Move myMove)	{
		Vector2 dest;
		switch (myMove) {
		case Move.UP:
			dest = coord + new Vector2 (0, 2);
			break;
		case Move.DOWN:
			dest = coord + new Vector2 (0, -2);
			break;
		case Move.LEFT:
			dest = coord + new Vector2 (-2, 0);
			break;
		case Move.RIGHT:
			dest = coord + new Vector2 (2, 0);
			break;
		default:
			throw new System.ArgumentOutOfRangeException ();
		}
		return dest;
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
