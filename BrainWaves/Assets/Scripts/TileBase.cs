using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class TileBase : MonoBehaviour 
{
	public abstract TileType GetTileType();
	public abstract void resetRoom();
}

public enum TileType {EMPTY, WALL, BUTTON, LEVER, DOOR, DEATH}