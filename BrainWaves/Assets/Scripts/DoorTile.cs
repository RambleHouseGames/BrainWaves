using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorTile : TileBase
{
	public override TileType GetTileType ()
	{
		return TileType.DOOR;
	}
}
