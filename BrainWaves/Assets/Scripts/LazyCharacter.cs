using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LazyCharacter : Character {

	private bool lazyThisTurn = false;

	void Awake()
	{
		InitPosition ();
	}

	override protected Move InterpretMove (Move yourMove) {
		if (lazyThisTurn)
			return Move.NONE;
		else
			return yourMove;
	}

	// Get the destination for a move.
	protected Vector2 GetDestination (Move myMove)	{
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
}
