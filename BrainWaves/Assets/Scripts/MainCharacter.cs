﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCharacter : Character
{

	private Move? queuedMove = null;

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

	override protected void onProgressChanged() {
		base.onProgressChanged();
		MovementAnimation.EndAllAnimations();
		foreach (var col in GameData.Instance.AllColumns) {
			Room newRoom = col.GetCurrentRoom ();
			if (newRoom.gameObject.activeInHierarchy) {
				col.character.resetRoom();
			}
		}
	}

	void Update()
	{
		if (GameData.Instance.GetCurrentGameState () != GameState.PLAYING)
			return;

		Move? move = null;
		if (Input.GetKeyDown (KeyCode.UpArrow)) {
			move = Move.UP;
		} else if (Input.GetKeyDown (KeyCode.DownArrow)) {
			move = Move.DOWN;
		} else if (Input.GetKeyDown (KeyCode.LeftArrow)) {
			move = Move.LEFT;
		} else if (Input.GetKeyDown (KeyCode.RightArrow)) {
			move = Move.RIGHT;
		}

		if (move.HasValue) {
			if (MovementAnimation.Animating) {
				// While animating, queue move.
				queuedMove = move;
				return;
			} else {
				// Else execute move.
				TryMove (move.Value);
			}
		}

		// Reset Level Key
		if (Input.GetKeyDown (KeyCode.R)) {
			GameData.Instance.onDeath (true);
		}
	}

	private void AnimationComplete() {
		GameData.Instance.ExecuteStateChanges();
		if (queuedMove.HasValue) {
			TryMove (queuedMove.Value);
			queuedMove = null;
		}
	}
}
