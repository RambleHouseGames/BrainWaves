using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rock : MonoBehaviour {

	public void Push (Room room, TileBase pushToTile, TileBase pushFromTile) {
		if (pushToTile.GetTileType() == TileType.BUTTON) {
			GameData.Instance.RegisterStateChange((pushToTile as ButtonTile).RockOn);
		}
		if (pushFromTile.GetTileType() == TileType.BUTTON) {
			GameData.Instance.RegisterStateChange((pushToTile as ButtonTile).RockOff);
		}

		transform.position = pushToTile.transform.position;
		transform.SetParent (pushToTile.transform);
	}
}
