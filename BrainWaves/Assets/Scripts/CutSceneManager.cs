using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class CutSceneManager : MonoBehaviour 
{
	[SerializeField]
	private List<Dialogue> dialogues;

	private bool cutSceneActive = false;
	private int lineNumber = 0;

	void Start()
	{
		GameData.Instance.AddProgressCallback (onProgressChanged);
	}

	private void onProgressChanged()
	{
		Debug.Log ("onProgressChanged");
		cutSceneActive = true;
		GetDialogueForProgress (GameData.Instance.GetCurrentProgress ()).lines [lineNumber].transform.Rotate(new Vector3(0, 0, 90f));
	}

	private Dialogue GetDialogueForProgress(int progressNumber)
	{
		foreach (Dialogue dialogue in dialogues) {
			if (dialogue.progressTrigger == progressNumber) {
				return dialogue;
			}
		}
		Debug.Assert (false, "No Dialogue for progress: " + progressNumber);
		return null;
	}

	void Update()
	{
		if (Input.GetKeyDown (KeyCode.Return)) {
			advanceDialogue ();
		}
	}

	private void advanceDialogue()
	{
		
	}
}

[Serializable]
public class Dialogue
{
	public int progressTrigger;
	public List<GameObject> lines;
}