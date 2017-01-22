using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeSortingLayerToCutScene : MonoBehaviour {

	void Awake()
	{
		foreach (Transform child in transform) {
			MeshRenderer renderer = child.GetComponent<MeshRenderer> ();
			if(renderer != null)
				renderer.sortingLayerName = "CutScene";
		}
	}
}
