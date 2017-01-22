using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathTile : TileBase
{
	public override TileType GetTileType ()
	{
		return TileType.DEATH;
	}
	public override void resetRoom(){}
}
