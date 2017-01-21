﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCharacter : Character
{

	void Awake()
	{
		InitPosition ();
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
		return yourMove;
	}

	void Update()
	{
		if (Input.GetKeyDown (KeyCode.UpArrow)) {
			TryMove (Move.UP);
			Debug.Log ("UP");
		} else if (Input.GetKeyDown (KeyCode.DownArrow)) {
			TryMove (Move.DOWN);
			Debug.Log ("Down");
		} else if (Input.GetKeyDown (KeyCode.LeftArrow)) {
			TryMove (Move.LEFT);
			Debug.Log ("Left");
		} else if (Input.GetKeyDown (KeyCode.RightArrow)) {
			TryMove (Move.RIGHT);
			Debug.Log ("Right");
		}
	}
}
