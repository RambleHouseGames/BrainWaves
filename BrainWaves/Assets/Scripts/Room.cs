using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Room : MonoBehaviour 
{
	[SerializeField]
	private List<RoomTile> tileLocators;

	public TileBase GetTile(Vector2 coord)
	{
		foreach (RoomTile tileLocator in tileLocators) {
			if (tileLocator.coord == coord) {
				TileBase tile = null;
				foreach (Transform child in tileLocator.tile.transform) {
					return child.gameObject.GetComponentInChildren<TileBase> () as TileBase;
				}
			}
		}
		Debug.Log ("Returning Null");
		return null;
	}
}

[Serializable]
public class RoomTile
{
	public Vector2 coord;
	public GameObject tile;
}