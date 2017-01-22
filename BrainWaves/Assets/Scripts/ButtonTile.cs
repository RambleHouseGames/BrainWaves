using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonTile : TileBase
{
	public DoorTile opens;

	private int pressers;

	public override TileType GetTileType ()
	{
		return TileType.BUTTON;
	}

	public void PressOn() {
		pressers += 1;
		if (pressers == 1) {
			if (opens != null)
				opens.Toggle ();
		}
	}

	public void PressOff() {
		pressers -= 1;
		if (pressers == 0) {
			if (opens != null)
				opens.Toggle ();
		}
	}
}
