using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameData : MonoBehaviour 
{
	[SerializeField]
	private List<RoomType> roomOrder = new List<RoomType> {RoomType.MAIN, RoomType.FLIP, RoomType.LAZY, RoomType.CRAZY};

	[SerializeField]
	private GameState currentGameState = GameState.PLAYING;

	[SerializeField]
	private int progress = 0;

	public GameState GetCurrentGameState()
	{
		return currentGameState;
	}

	public int GetCurrentProgress()
	{
		return progress;
	}

	public int GetPosition(RoomType type)
	{
		for(int i = 0; i < roomOrder.Count; i++) {
			if (roomOrder [i] == type)
				return i;
		}
		Debug.Assert (false, "Unable to find position for room type: " + type.ToString());
		return 0;
	}

	public void MoveRoom(RoomType type, int destination)
	{
		Debug.Assert (destination > 0, "Only Main Room can be in first position");
		Debug.Assert (destination < roomOrder.Count, "Trying to move room to invalid position");

		roomOrder.Remove (type);
		roomOrder.Insert (destination, type);
	}

	public void NotifyProgressAnimationComplete(RoomType room)
	{
		if(currentGameState == GameState.ADVANCING)
			Debug.Log ("" + room + " Room finished advancing");
	}
}

public enum RoomType {MAIN, FLIP, LAZY, CRAZY}
public enum GameState {PLAYING, ADVANCING, CUT_SCENE}