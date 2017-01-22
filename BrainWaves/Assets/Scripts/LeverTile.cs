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
		if (opens != null) {
			opens.Toggle (); changeOpen ();
		}
	}
	public override void resetRoom(){}

	private Sprite defaultSprite;

	public void Start(){
		SpriteRenderer renderer = gameObject.GetComponent<SpriteRenderer> ();
		defaultSprite = renderer.sprite;
	}
	private void changeOpen(){
		SpriteRenderer renderer = gameObject.GetComponent<SpriteRenderer> ();
		if (opens.open) {
			renderer.sprite = GameData.Instance.levelOn;
		} else {
			renderer.sprite = defaultSprite;
		}
	}
}
