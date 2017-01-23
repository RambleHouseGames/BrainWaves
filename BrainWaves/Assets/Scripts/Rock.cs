using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rock : MonoBehaviour {
	
	private TileBase startTile;
	
	public void Start(){
		SpriteRenderer renderer = gameObject.AddComponent<SpriteRenderer> ();
		renderer.sprite = GameData.Instance.rockSprite; renderer.sortingOrder = 1;
		TextMesh text = renderer.gameObject.GetComponentInChildren<TextMesh> ();
		if (text != null)
			GameObject.Destroy (text.gameObject);
	}
	
	public void resetRoom(){
		if (startTile != null) {
			transform.position = startTile.transform.position;
			transform.SetParent (startTile.transform);
		}
	}
	
	public void Push (Room room, TileBase pushToTile, TileBase pushFromTile, float delay) {
		if (startTile == null) {
			startTile = pushFromTile;
		}
		if (pushToTile.GetTileType() == TileType.BUTTON) {
			GameData.Instance.RegisterStateChange((pushToTile as ButtonTile).RockOn);
		}
		if (pushFromTile.GetTileType() == TileType.BUTTON) {
			GameData.Instance.RegisterStateChange((pushFromTile as ButtonTile).RockOff);
		}

		StartCoroutine(MovementAnimation.SlideTo(transform, pushToTile.transform.position, delay));
		transform.SetParent (pushToTile.transform);
	}
}
