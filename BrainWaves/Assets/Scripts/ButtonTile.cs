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
		Debug.Assert (playered == false, "Trying to player Activate an Active Button");
		playered = true;
		if(!rocked) opens.SetOpen (true);
	}

	public void PlayerOff()
	{
		Debug.Assert (playered == true, "Called PlayerOff on a button with no player on it");
		playered = false;
		if(!rocked) opens.SetOpen (false);
	}

	public void RockOn()
	{
		Debug.Assert (rocked == false, "Trying to rock Activate an Active Button");
		rocked = true;
		if(!playered) opens.SetOpen (true);
	}

	public void RockOff()
	{
		Debug.Assert (rocked == true, "tried");
		rocked = false;
		if(!playered) opens.SetOpen (false);
	}
	public override void resetRoom(){
		rocked = false; playered = false;
	}
}
