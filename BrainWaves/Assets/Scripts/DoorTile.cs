using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorTile : TileBase
{
	[SerializeField]
	private bool open = false;

	public override TileType GetTileType ()
	{
		if (open)
			return TileType.EMPTY;
		else
			return TileType.DOOR;
	}

	public void Toggle() {
		open = !open;
		if (open)
			AudioManager.Instance.PlayOpenDoor ();
		else
			AudioManager.Instance.PlayCloseDoor ();
	}

	public void SetOpen(bool value)
	{
		open = value;
		if (open)
			AudioManager.Instance.PlayOpenDoor ();
		else
			AudioManager.Instance.PlayCloseDoor ();
	}
	public override void resetRoom(){
		open = false;
	}
}
