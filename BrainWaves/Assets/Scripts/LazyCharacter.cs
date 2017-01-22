using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LazyCharacter : Character {

	private bool lazyThisTurn = false;

	[SerializeField]
	private GameObject sleepIndicator;

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
	override protected bool TryMove(Move yourMove, int tiles = 1, float delay = 0f) {
		if (base.TryMove (yourMove, tiles, delay)) {
			lazyThisTurn = !lazyThisTurn;
			sleepIndicator.SetActive (lazyThisTurn);
			return true;
		} else return false;
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
