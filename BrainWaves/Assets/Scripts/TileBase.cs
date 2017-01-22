using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class TileBase : MonoBehaviour 
{
	public abstract TileType GetTileType();
	public abstract void resetRoom();
}

public enum TileType {EMPTY, WALL, BUTTON, LEVER, DOOR, DEATH}

public static class TileTypeExtension {

	public static bool IsWalkable(this TileType tileType) {
		switch (tileType) {
			case TileType.WALL:
			case TileType.LEVER:
			case TileType.DOOR:
				return false;
			case TileType.EMPTY:
			case TileType.BUTTON:
			case TileType.DEATH:
				return true;
			default:
				throw new System.ArgumentOutOfRangeException ();
		}
	}

	public static bool TypeEquals(this TileType tileType, TileBase that) {
		return that != null && that.GetTileType() == tileType;
	}
}
	