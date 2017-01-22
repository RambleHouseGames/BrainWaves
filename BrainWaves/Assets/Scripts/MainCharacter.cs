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
	}

	public override RoomType GetRoomType ()
	{
		return RoomType.MAIN;
	}

	void Update()
	{
//		if (GameData.Instance.GetCurrentGameState () != GameState.PLAYING)
//			return;

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

		// Second phase: resolve state changes.
		if (moveAttempted) {
			GameData.Instance.ExecuteStateChanges ();
		}

		// Reset Level Key
		if (Input.GetKeyDown (KeyCode.R)) {
			GameData.Instance.onDeath (true);
		}
	}

	private void onProgressChanged()
	{
		Room newRoom = myCollumn.GetCurrentRoom ();
		TileBase tile = newRoom.GetTile (new Vector2(4, 0));
		transform.position = tile.transform.position;
		transform.SetParent (tile.transform);
		coord = new Vector2 (4, 0);
	}
}
