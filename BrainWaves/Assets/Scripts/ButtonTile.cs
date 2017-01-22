using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonTile : TileBase
{
	public DoorTile opens;

	private bool rocked = false;
	private bool playered = false;

	public override TileType GetTileType ()
	{
		return TileType.BUTTON;
	}

	public void PlayerOn()
	{
		Debug.Assert (playered == false && rocked == false, "Trying to player Activate an Active Button");
		playered = true;
		opens.SetOpen (true);
	}

	public void PlayerOff()
	{
		Debug.Assert (playered == true, "Called PlayerOff on a button with no player on it");
		playered = false;
		opens.SetOpen (false);
	}

	public void RockOn()
	{
		Debug.Assert (playered == false && rocked == false, "Trying to rock Activate an Active Button");
		rocked = true;
		opens.SetOpen (true);
	}

	public void RockOff()
	{
		Debug.Assert (rocked == false, "tried");
		rocked = false;
		opens.SetOpen (false);
	}
}
