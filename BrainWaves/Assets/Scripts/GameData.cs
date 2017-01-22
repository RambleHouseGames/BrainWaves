using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GameData : MonoBehaviour 
{
	public static GameData Instance { get; private set; }

	[SerializeField]
	private List<RoomType> roomOrder = new List<RoomType> {RoomType.MAIN, RoomType.FLIP, RoomType.LAZY, RoomType.CRAZY};

	[SerializeField]
	private GameState currentGameState = GameState.PLAYING;

	[SerializeField]
	private int progress = 0;

	[SerializeField]
	private List<PuzzleRequirement> puzzleRequirements;

	private Action progressCallback;

	private List<RoomType> victoryReports;
	private List<RoomType> advanceCompleteReports;

	private List<Action> stateChangeQueue = new List<Action>();

	[Header("Collumns")]
	public RoomCollumn mainCol;
	public RoomCollumn flipCol;
	public RoomCollumn lazyCol;
	public RoomCollumn crazyCol;

	[Header("Prefabs")]
	public List<TilePrefab> tilePrefabs;
	public GameObject rockPrefab;

	void Awake() {
		Instance = this;
	}

	public void AddProgressCallback(Action callback)
	{
		if (progressCallback == null)
			progressCallback = callback;
		else
			progressCallback += callback;
	}

	public void RemoveProgressCallback(Action callback)
	{
		progressCallback -= callback;
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
		if (myI == 3)
			return null;
		else
			return GetRoomCollumn(roomOrder [myI + 1]);
	}

	public void NotifyProgressAnimationComplete(RoomType room)
	{
		Debug.Assert (currentGameState == GameState.ADVANCING, "Finished Advancing but not Advancing");
		if (advanceCompleteReports == null)
			advanceCompleteReports = new List<RoomType> ();
		//Debug.Assert (!advanceCompleteReports.Contains(room), "already finished: " + room);
		advanceCompleteReports.Add (room);

	}

	public void ReportExitDoor(RoomType type)
	{
		if (victoryReports == null || victoryReports.Contains(type))
			victoryReports = new List<RoomType> ();
		
		victoryReports.Add (type);

		PuzzleRequirement requirement = getRequirement (progress);
		foreach (RoomType room in requirement.rooms) {
			if (!victoryReports.Contains (room))
				return;
		}
		progress++;
		//currentGameState = GameState.ADVANCING;
		if (progressCallback != null)
			progressCallback ();
	}

	private PuzzleRequirement getRequirement(int progressNumber)
	{
		foreach (PuzzleRequirement puzzleRequirement in puzzleRequirements) {
			if (puzzleRequirement.progressNumber == progressNumber)
				return puzzleRequirement;
		}
		Debug.Assert (false, "No Puzzle Requirements found for progressNumber: " + progressNumber);
		return null;
	}

	// State change queue
	public void RegisterStateChange(Action action) {
		stateChangeQueue.Add (action);
	}
	public void ExecuteStateChanges() {
		//Debug.Log ("executing state changes: " + stateChangeQueue.Count);
		stateChangeQueue.ForEach (x => x());
		stateChangeQueue.Clear ();
	}
}

public enum RoomType {MAIN, FLIP, LAZY, CRAZY}
public enum GameState {PLAYING, ADVANCING, CUT_SCENE}
public enum Move {NONE, UP, DOWN, LEFT, RIGHT}

[Serializable]
public class PuzzleRequirement
{
	public int progressNumber;
	public List<RoomType> rooms;
}

[Serializable]
public class TilePrefab
{
	public int CSVCode;
	public GameObject prefab;
}