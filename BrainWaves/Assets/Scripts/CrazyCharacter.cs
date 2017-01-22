using System.Collections;
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

	override protected bool TryMove(Move yourMove, int tiles) {
		return base.TryMove(yourMove, tiles * 2);
	}
}
