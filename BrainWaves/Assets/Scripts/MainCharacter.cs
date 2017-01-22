using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCharacter : Character
{

	void Awake()
	{
		InitPosition ();
	}

	void Start()
	{
		GameData.Instance.AddProgressCallback (onProgressChanged);
		MovementAnimation.DoneAnimating += AnimationComplete;
	}

	public override RoomType GetRoomType ()
	{
		return RoomType.MAIN;
	}

	void Update()
	{
		if (GameData.Instance.GetCurrentGameState () != GameState.PLAYING)
			return;

		// While animating, don't accept user input.
		if (MovementAnimation.Animating)
			return;

		// First phase: move characters and register state changes.
		bool moveAttempted = false;
		if (Input.GetKeyDown (KeyCode.UpArrow)) {
			moveAttempted = true;
			TryMove (Move.UP, 1);
		} else if (Input.GetKeyDown (KeyCode.DownArrow)) {
			moveAttempted = true;
			TryMove (Move.DOWN, 1);
		} else if (Input.GetKeyDown (KeyCode.LeftArrow)) {
			moveAttempted = true;
			TryMove (Move.LEFT, 1);
		} else if (Input.GetKeyDown (KeyCode.RightArrow)) {
			moveAttempted = true;
			TryMove (Move.RIGHT, 1);
		}

		// Second phase: resolve state changes will happen when animations complete.

		// Reset Level Key
		if (Input.GetKeyDown (KeyCode.R)) {
			GameData.Instance.onDeath (true);
		}
	}

	private void AnimationComplete() {
		Debug.Log("State change");
		GameData.Instance.ExecuteStateChanges();
	}
}
