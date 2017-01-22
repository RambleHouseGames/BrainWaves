using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorTile : TileBase
{
	[SerializeField]
	public bool open = false;

	public override TileType GetTileType ()
	{
		if (open)
			return TileType.EMPTY;
		else
			return TileType.DOOR;
	}

	public void Toggle() {
		open = !open; changeOpen ();
		if (open)
			AudioManager.Instance.PlayOpenDoor ();
		else
			AudioManager.Instance.PlayCloseDoor ();
	}

	public void SetOpen(bool value)
	{
		open = value; changeOpen ();
		if (open)
			AudioManager.Instance.PlayOpenDoor ();
		else
			AudioManager.Instance.PlayCloseDoor ();
	}
	public override void resetRoom(){
		open = false; changeOpen ();
	}

	private Sprite defaultSprite;

	public void Start(){
		SpriteRenderer renderer = gameObject.GetComponent<SpriteRenderer> ();
		defaultSprite = renderer.sprite;
	}
	private void changeOpen(){
		SpriteRenderer renderer = gameObject.GetComponent<SpriteRenderer> ();
		if (open) {
			renderer.sprite = GameData.Instance.doorOpen;
		} else {
			renderer.sprite = defaultSprite;
		}
	}
}
