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

	void Awake()
	{
		if (import == false)
			return;

		int x = 0;
		int y = 20;
		if (import) {
			string csv = File.ReadAllText (Application.streamingAssetsPath + "/" + CSVPath);
			int i = 0;
			string value = "";
			while (i < csv.Length) {
				if (csv [i] == ',') {
					if (value.Length > 0) {
						InstantiateTileForCSVValue (int.Parse (value), new Vector2 (x, y));
					}
					x++;
					value = "";
				}
				else if (csv [i] == '\n') {
					if (value.Length > 0) {
						InstantiateTileForCSVValue (int.Parse (value), new Vector2 (x, y));
					}
					y--;
					x = 0;
					value = "";
				}
				else
					value += csv [i];
				i++;
			}
		}
	}

	public TileBase GetTile(Vector2 coord)
	{
		foreach (RoomTile tileLocator in tileLocators) {
			if (tileLocator.coord == coord) {
				TileBase tile = null;
				Debug.Assert (tileLocator.tile != null, "no tile in locator: " + coord);
				foreach (Transform child in tileLocator.tile.transform) {
					TileBase returnValue = child.gameObject.GetComponentInChildren<TileBase> () as TileBase;
					if (returnValue != null)
						return returnValue;
				}
			}
		}
		//Debug.Log ("" + gameObject.name + "Returning Null for coord: " + coord);
		return null;
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

		if (value == 5) {
			// Rock
			var rock = GameObject.Instantiate (GameData.Instance.rockPrefab, locator.transform.position, Quaternion.identity, locator);
			rock.transform.localScale = Vector3.one;
		}
	}
}

[Serializable]
public class RoomTile
{
	public Vector2 coord;
	public GameObject tile;
}
