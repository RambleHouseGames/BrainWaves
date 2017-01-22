using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class CutSceneManager : MonoBehaviour 
{
	[SerializeField]
	private List<Dialogue> dialogues;

	void Start()
	{
		GameData.Instance.AddProgressCallback (onProgressChanged);
	}

	private void onProgressChanged()
	{
		
	}
}

[Serializable]
public class Dialogue
{
	public int progressTrigger;
	public List<GameObject> lines;
}