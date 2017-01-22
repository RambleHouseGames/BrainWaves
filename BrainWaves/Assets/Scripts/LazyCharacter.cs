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
	override protected bool TryMove(Move yourMove, int tiles) {
		if(base.TryMove(yourMove, tiles)) lazyThisTurn = !lazyThisTurn;
	}

	override protected Move InterpretMove (Move yourMove) {
		Move move;
		if (lazyThisTurn)
			move = Move.NONE;
		else
			move = yourMove;
		return move;
	}
}
