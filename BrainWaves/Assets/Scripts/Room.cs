using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;

public class Room : MonoBehaviour 
{
	[SerializeField]
	private string CSVPath;

	[SerializeField]
	private bool import;

	[SerializeField]
	private List<RoomTile> tileLocators;

	[SerializeField]
	private List<TileSprite> tileSprites;

	void Awake()
	{
		if (import == false)
			return;

		foreach (RoomTile roomTile in tileLocators) {
			fixTile (roomTile.tile.GetComponentInChildren<TileBase>());
		}

//		int x = 0;
//		int y = 20;
//		if (import) {
//			string csv = File.ReadAllText (Application.streamingAssetsPath + "/" + CSVPath);
//			int i = 0;
//			string value = "";
//			while (i < csv.Length) {
//				if (csv [i] == ',') {
//					if (value.Length > 0) {
//						InstantiateTileForCSVValue (int.Parse (value), new Vector2 (x, y));
//					}
//					x++;
//					value = "";
//				}
//				else if (csv [i] == '\n') {
//					if (value.Length > 0) {
//						InstantiateTileForCSVValue (int.Parse (value), new Vector2 (x, y));
//					}
//					y--;
//					x = 0;
//					value = "";
//				}
//				else
//					value += csv [i];
//				i++;
//			}
//		}
	}
	public void restartRoom(){
		foreach (RoomTile tileLocator in tileLocators) {
			if (tileLocator.tile != null) {
				foreach (Transform child in tileLocator.tile.transform) {
					TileBase b = child.gameObject.GetComponentInChildren<TileBase> () as TileBase;
					if (b != null) {
						b.resetRoom ();
						Rock rock = GetRock (b);
						if (rock != null)
							rock.resetRoom ();
					}
				}
				GameData.Instance.mainCol.character.resetRoom ();
				GameData.Instance.flipCol.character.resetRoom ();
				GameData.Instance.lazyCol.character.resetRoom ();
				GameData.Instance.crazyCol.character.resetRoom ();
			}
		}
	}

	public TileBase GetTile(Vector2 coord)
	{
		foreach (RoomTile tileLocator in tileLocators) {
			if (tileLocator.coord == coord) {
				Debug.Assert (tileLocator.tile != null, "no tile in locator: " + coord);
				foreach (Transform child in tileLocator.tile.transform) {
					TileBase returnValue = child.gameObject.GetComponentInChildren<TileBase> () as TileBase;
					if (returnValue != null)
						return returnValue;
				}
				//return null;
			}
		}
		//Debug.Log ("" + gameObject.name + "Returning Null for coord: " + coord);
		return null;
	}

	public Vector2 GetCoord(TileBase tile) {
		foreach (RoomTile tileLocator in tileLocators) {
			if (tileLocator.tile == tile.transform.parent.gameObject) {
				return tileLocator.coord;
			}
		}
		throw new ArgumentException ("That tile does not belong in this room: can't get it's coord.");
	}

	public Rock GetRock(TileBase tile) {
		if (tile == null)
			return null;

		var testPos = tile.transform.position;
		var hit = Physics2D.OverlapPoint (testPos, LayerMask.GetMask ("Rock"));
		if (hit == null)
			return null;
		else
			return hit.GetComponent<Rock>();
	}

	private GameObject GetTilePrefab(int CSVCode)
	{
		foreach (TilePrefab tilePrefab in GameData.Instance.tilePrefabs) {
			if (tilePrefab.CSVCode == CSVCode)
				return tilePrefab.prefab;
		}
		Debug.Assert (false, "No Prefab Found for CSV Code: " + CSVCode);
		return null;
	}

	private void InstantiateTileForCSVValue(int value, Vector2 coord)
	{
		Transform locator = GetTile (coord).transform.parent;
		foreach (Transform child in locator) {
			TileBase tileBase = child.GetComponent<TileBase> ();
			if (tileBase != null)
				GameObject.Destroy (tileBase.gameObject);
		}
		GameObject newGO = GameObject.Instantiate (GetTilePrefab(value), locator.transform.position, Quaternion.identity, locator);
		newGO.transform.localScale = Vector3.one;

		//if (value == 5) {
		//	// Rock
		//	var rock = GameObject.Instantiate (GameData.Instance.rockPrefab, locator.transform.position, Quaternion.identity, locator);
		//	rock.transform.localScale = Vector3.one;
		//}
	}

	private void fixTile(TileBase tile)
	{
		GameObject go = tile.gameObject;
		Destroy (tile);
		TileBase childTile;
		foreach (Transform child in go.transform) {
			if (child.GetComponent<TileBase> () != null) {
				childTile = child.GetComponent<TileBase> ();
				foreach (TileSprite tileSprite in tileSprites) {
					if (tileSprite.type == childTile.GetTileType ()) {
						SpriteRenderer renderer = childTile.GetComponent<SpriteRenderer> ();
						Debug.Log ("Fixing: " + childTile.gameObject.name);
						renderer.sprite = tileSprite.sprite;
						TextMesh text = renderer.gameObject.GetComponentInChildren<TextMesh> ();
						if (text != null)
							GameObject.Destroy (text.gameObject);
					} else
						Debug.Log ("type: " + childTile.GetTileType ());
				}
			}
		}
	}
}

[Serializable]
public class RoomTile
{
	public Vector2 coord;
	public GameObject tile;
}

[Serializable]
public class TileSprite
{
	public TileType type;
	public Sprite sprite;
}
