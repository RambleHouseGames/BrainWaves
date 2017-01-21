using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCharacter : MonoBehaviour 
{
	[SerializeField]
	private GameData gameData;

	[SerializeField]
	private RoomCollumn myCollumn;

	[SerializeField]
	private Vector2 coord;

	void Awake()
	{
		TileBase destinationTile = myCollumn.GetCurrentRoom().GetTile(coord);
		if (destinationTile.GetTileType () == TileType.EMPTY) {
			transform.position = destinationTile.transform.position;
		}
	}

	void Update()
	{
		Vector2 destination = coord;
		if (Input.GetKeyDown (KeyCode.UpArrow)) {
			destination = coord + new Vector2 (0, 1);
			Debug.Log ("UP");
		} else if (Input.GetKeyDown (KeyCode.DownArrow)) {
			destination = coord + new Vector2 (0, -1);
			Debug.Log ("Down");
		} else if (Input.GetKeyDown (KeyCode.LeftArrow)) {
			destination = coord + new Vector2 (-1, 0);
			Debug.Log ("Left");
		} else if (Input.GetKeyDown (KeyCode.RightArrow)) {
			destination = coord + new Vector2 (1, 0);
			Debug.Log ("Right");
		} else
			return;

		Debug.Log ("Moving From " + coord + " To " + destination);

		TileBase destinationTile = myCollumn.GetCurrentRoom().GetTile(destination);
		if (destinationTile != null && destinationTile.GetTileType () == TileType.EMPTY) {
			transform.position = destinationTile.transform.position;
			transform.SetParent (destinationTile.transform);
			coord = destination;
		}
	}
}
