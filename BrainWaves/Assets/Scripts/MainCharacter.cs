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

	override protected Move InterpretMove (Move yourMove) {
		return yourMove;
	}

	void Update()
	{
		if (GameData.Instance.GetCurrentGameState () != GameState.PLAYING)
			return;

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

	private void onProgressChanged()
	{
		Room newRoom = myCollumn.GetCurrentRoom ();
		TileBase tile = newRoom.GetTile (new Vector2(4, 0));
		transform.position = tile.transform.position;
		transform.SetParent (tile.transform);
		coord = new Vector2 (4, 0);
	}
}
