using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmptyTile : TileBase
{
	public override TileType GetTileType ()
	{
		return TileType.EMPTY;
	}
	public override void resetRoom(){}
}
