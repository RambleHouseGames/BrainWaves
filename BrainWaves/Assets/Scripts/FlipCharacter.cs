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
			case Move.NONE:
				return Move.NONE;
			default:
				throw new System.ArgumentOutOfRangeException ();
		}
	}
}
