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

	private GameObject activeLine = null;

	void Start()
	{
		GameData.Instance.AddProgressCallback (onProgressChanged);
		onProgressChanged ();
	}

	void Update()
	{
		if (Input.GetKeyDown (KeyCode.Return) && activeLine != null) {
			advanceDialogue ();
		}
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
		activeLine = GameObject.Instantiate (activeDialogue.lines[currentLine]);
		activeLine.transform.SetParent (transform);
		activeLine.transform.position = new Vector3 (activeLine.transform.position.x, activeLine.transform.position.y, transform.position.z);
	}

	private void advanceDialogue()
	{
		GameObject.Destroy (activeLine);
		currentLine++;
		if (currentLine >= activeDialogue.lines.Count) {
			activeLine = null;
			activeDialogue = null;
			currentLine = 0;
			GameData.Instance.ReportCutSceneComplete ();
		} else {
			activeLine = GameObject.Instantiate (activeDialogue.lines [currentLine]);
			activeLine.transform.SetParent (transform);
			activeLine.transform.position = new Vector3 (activeLine.transform.position.x, activeLine.transform.position.y, transform.position.z);
		}
	}
}

[Serializable]
public class Dialogue
{
	public int progressTrigger;
	public List<GameObject> lines;
}