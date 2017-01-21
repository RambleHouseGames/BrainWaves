﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlipCharacter : Character {

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
		return RoomType.FLIP;
	}

	override protected Vector2 GetDestination (Move myMove)
	{
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

	override protected Move InterpretMove (Move yourMove) {
		switch (yourMove) {
			case Move.UP:
				return Move.UP;
			case Move.DOWN:
				return Move.DOWN;
			case Move.LEFT:
				return Move.RIGHT;
			case Move.RIGHT:
				return Move.LEFT;
			default:
				throw new System.ArgumentOutOfRangeException ();
		}
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
