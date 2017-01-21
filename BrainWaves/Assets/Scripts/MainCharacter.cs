using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCharacter : Character
{

	void Awake()
	{
		InitPosition ();
	}

	override protected Move InterpretMove (Move yourMove) {
		return yourMove;
	}

	void Update()
	{
		if (Input.GetKeyDown (KeyCode.UpArrow)) {
			TryMove (Move.UP);
		} else if (Input.GetKeyDown (KeyCode.DownArrow)) {
			TryMove (Move.DOWN);
		} else if (Input.GetKeyDown (KeyCode.LeftArrow)) {
			TryMove (Move.LEFT);
		} else if (Input.GetKeyDown (KeyCode.RightArrow)) {
			TryMove (Move.RIGHT);
		}
	}
}
