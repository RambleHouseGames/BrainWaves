using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrazyCharacter : Character {

	void Awake()
	{
		InitPosition ();
	}

	override protected Move InterpretMove (Move yourMove) {
		return yourMove;
	}
}
