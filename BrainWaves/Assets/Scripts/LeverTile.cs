using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeverTile : TileBase
{
	public DoorTile opens;

	public override TileType GetTileType ()
	{
		return TileType.LEVER;
	}

	public void Trigger() {
		opens.Toggle ();
	}
}
