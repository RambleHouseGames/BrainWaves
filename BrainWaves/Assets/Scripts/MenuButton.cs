using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuButton : MonoBehaviour 
{
	[SerializeField]
	private GameObject hover;

	[SerializeField]
	private GameObject unhover;

	[SerializeField]
	private MenuButtonType type;

	private bool isHover = false;

	void Update()
	{
		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		RaycastHit hit = new RaycastHit ();

		if (Physics.Raycast (ray, out hit)) {
			if (Input.GetMouseButtonDown (0) && hit.collider.gameObject == gameObject) {
				activateButton ();
			}
			isHover = hit.collider.gameObject == gameObject;
		}
		else
			isHover = false;

		hover.SetActive (isHover);
		unhover.SetActive (!isHover);
	}

	private void activateButton()
	{
		switch (type) {
		case MenuButtonType.START:
			SceneManager.LoadScene ("GameScene", LoadSceneMode.Single);
			return;
		case MenuButtonType.ABOUT:
			Debug.Log ("The Most Awesomest Game What Ever Existed!!");
			return;
		case MenuButtonType.QUIT:
			Application.Quit ();
			return;
		default:
			Debug.Assert (false, "unknown button type: " + type);
			return;
		}
	}
}

public enum MenuButtonType {START, ABOUT, QUIT}