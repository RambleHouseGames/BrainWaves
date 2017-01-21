﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameData : MonoBehaviour 
{
	public static GameData Instance { get; private set; }

	[SerializeField]
	private List<RoomType> roomOrder = new List<RoomType> {RoomType.MAIN, RoomType.FLIP, RoomType.LAZY, RoomType.CRAZY};

	[SerializeField]
	private GameState currentGameState = GameState.PLAYING;

	[SerializeField]
	private int progress = 0;

	[Header("Collumns")]
	public RoomCollumn mainCol;
	public RoomCollumn flipCol;
	public RoomCollumn lazyCol;
	public RoomCollumn crazyCol;

	void Awake() {
		Instance = this;
	}

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

	public RoomCollumn GetRoomCollumn(RoomType roomType) {
		switch (roomType) {
			case RoomType.MAIN:
				return mainCol;
			case RoomType.FLIP:
				return flipCol;
			case RoomType.LAZY:
				return lazyCol;
			case RoomType.CRAZY:
				return crazyCol;
			default:
				throw new System.ArgumentOutOfRangeException ();
		}
	}

	public RoomCollumn GetNextRoomCollumn(RoomType myRoomType) {
		int myI = roomOrder.IndexOf (myRoomType);
		Debug.Log (myI);
		if (myI == 3)
			return null;
		else
			return GetRoomCollumn(roomOrder [myI + 1]);
	}

	public void NotifyProgressAnimationComplete(RoomType room)
	{
		if(currentGameState == GameState.ADVANCING)
			Debug.Log ("" + room + " Room finished advancing");
	}
}

public enum RoomType {MAIN, FLIP, LAZY, CRAZY}
public enum GameState {PLAYING, ADVANCING, CUT_SCENE}
public enum Move {NONE, UP, DOWN, LEFT, RIGHT}