using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class CutSceneManager : MonoBehaviour 
{
	[SerializeField]
	private List<Dialogue> dialogues;

	private Dialogue activeDialogue = null;
	private int currentLine = 0;

	void Start()
	{
		GameData.Instance.AddProgressCallback (onProgressChanged);
		onProgressChanged ();
	}

	private void onProgressChanged()
	{
		int progressNumber = GameData.Instance.GetCurrentProgress ();
		foreach (Dialogue dialogue in dialogues) {
			if (dialogue.progressTrigger == progressNumber) {
				activeDialogue = dialogue;
				startDialogue ();
			}
		}
	}

	private void startDialogue()
	{
		GameObject.Instantiate (activeDialogue.lines[currentLine]);
	}
}

[Serializable]
public class Dialogue
{
	public int progressTrigger;
	public List<GameObject> lines;
}