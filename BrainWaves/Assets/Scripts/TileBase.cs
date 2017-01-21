using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class TileBase : MonoBehaviour 
{
	public abstract TileType GetTileType();
}

public enum TileType {EMPTY, WALL, BUTTON, LEVER}