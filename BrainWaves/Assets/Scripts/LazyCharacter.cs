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
		Move move;
		if (lazyThisTurn)
			move = Move.NONE;
		else
			move = yourMove;
		lazyThisTurn = !lazyThisTurn;
		return move;
	}
}
