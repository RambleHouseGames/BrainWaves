using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallTile : TileBase
{
	public override TileType GetTileType ()
	{
		return TileType.WALL;
	}
	public override void resetRoom(){}
}
