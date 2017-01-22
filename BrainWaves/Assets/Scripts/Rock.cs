﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rock : MonoBehaviour {
	
	private TileBase startTile;
	
	public void Start(){
		startTile = transform.parent.gameObject.GetComponent<TileBase>();
	}
	
	public void resetRoom(){
		transform.position = startTile.transform.position;
		transform.SetParent (startTile.transform);
	}
	
	public void Push (Room room, TileBase pushToTile, TileBase pushFromTile) {
		if (pushToTile.GetTileType() == TileType.BUTTON) {
			GameData.Instance.RegisterStateChange((pushToTile as ButtonTile).RockOn);
		}
		if (pushFromTile.GetTileType() == TileType.BUTTON) {
			GameData.Instance.RegisterStateChange((pushFromTile as ButtonTile).RockOff);
		}

		StartCoroutine(MovementAnimation.SlideTo(transform, pushToTile.transform.position));
		transform.SetParent (pushToTile.transform);
	}
}
