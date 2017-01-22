using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrazyCharacter : Character {

	void Awake()
	{
		InitPosition ();
	}

	void Start()
	{
		GameData.Instance.AddProgressCallback (onProgressChanged);
	}

	public override RoomType GetRoomType ()
	{
		return RoomType.CRAZY;
	}

	override protected void FindLegalMove(Move myMove, out TileBase entering, out TileBase bumpingInto) {
		entering = null;
		bumpingInto = null;

		// Tile 2 spaces away.
		var tile2 = myCollumn.GetCurrentRoom ().GetTile (MoveBy (coord, myMove, 2));
		// Tile 1 space away.
		var tile1 = myCollumn.GetCurrentRoom ().GetTile (MoveBy (coord, myMove, 1));

		if (tile2 == null && tile1 == null) {
			// Both null, so entering and bumping are null.
			return;
		} else if (tile1 == null || !tile1.GetTileType ().IsWalkable()) {
			// tile1 is not walkable, so we bump into it (or it's null)
			bumpingInto = tile1;
		} else if (tile2 == null || !tile2.GetTileType ().IsWalkable()) {
			// tile1 is walkable, but not tile2, so we bump into tile2 (or it's null)
			entering = tile1;
			bumpingInto = tile2;
		} else {
			// tile1 and tile2 are walkable and not null, so enter tile2, no bumping
			entering = tile2;
		}
	}
}
