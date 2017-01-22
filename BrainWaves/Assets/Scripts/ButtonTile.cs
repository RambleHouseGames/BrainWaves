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
		playered = true; changeOpen ();
		if(!rocked) opens.SetOpen (true);
	}

	public void PlayerOff()
	{
		Debug.Assert (playered == true, "Called PlayerOff on a button with no player on it");
		playered = false; changeOpen ();
		if(!rocked) opens.SetOpen (false);
	}

	public void RockOn()
	{
		Debug.Assert (rocked == false, "Trying to rock Activate an Active Button");
		rocked = true; changeOpen ();
		if(!playered) opens.SetOpen (true);
	}

	public void RockOff()
	{
		Debug.Assert (rocked == true, "tried");
		rocked = false; changeOpen ();
		if(!playered) opens.SetOpen (false);
	}
	public override void resetRoom(){
		rocked = false; playered = false; changeOpen ();
	}
	private Sprite defaultSprite;

	public void Start(){
		SpriteRenderer renderer = gameObject.GetComponent<SpriteRenderer> ();
		defaultSprite = renderer.sprite;
	}
	private void changeOpen(){
		SpriteRenderer renderer = gameObject.GetComponent<SpriteRenderer> ();
		if (rocked || playered) {
			renderer.sprite = GameData.Instance.buttonDown;
		} else {
			renderer.sprite = defaultSprite;
		}
	}
}
