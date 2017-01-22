using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rock : MonoBehaviour {

	public void Push (Room room, TileBase pushToTile, TileBase pushFromTile) {
		if (pushToTile.GetTileType() == TileType.BUTTON) {
			(pushToTile as ButtonTile).PressOn ();
		}
		if (pushFromTile.GetTileType() == TileType.BUTTON) {
			(pushToTile as ButtonTile).PressOff ();
		}

		transform.position = pushToTile.transform.position;
		transform.SetParent (pushToTile.transform);
	}
}
