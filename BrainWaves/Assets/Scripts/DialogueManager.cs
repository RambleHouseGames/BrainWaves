using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class DialogueManager : MonoBehaviour 
{
	[SerializeField]
	private List<CutScene> cutScenes;

	void Awake()
	{
		GameData.Instance.AddProgressCallback (onProgressChanged);
	}

	private void onProgressChanged()
	{
		
	}
}

[Serializable]
public class CutScene
{
	public int progressTrigger;
	public List<DialoguePopup> dialoguePopups;
}