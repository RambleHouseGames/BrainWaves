using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovableRoom : MonoBehaviour 
{
	[SerializeField]
	private GameData GameData;

	[SerializeField]
	private RoomType myType;

	[SerializeField]
	private GameObject startingLocator;

	[SerializeField]
	private List<GameObject> locators;

	[SerializeField]
	private float minSpeed = 1f;

	[SerializeField]
	LayerMask RoomColliderMask;

	[SerializeField]
	LayerMask SliderColliderMask;

	private bool amSelected = false;
	private Vector3 SelectionStart;
	private Vector3 StartPosition;

	void Update()
	{
		if (amSelected) {
			if (Input.GetMouseButtonUp (0)) {
				amSelected = false;
			} else {
				Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
				RaycastHit hit = new RaycastHit ();
				if (Physics.Raycast (ray, out hit, 1000f, SliderColliderMask)) {
					Vector2 moveFactor = new Vector2 (hit.point.x - SelectionStart.x, hit.point.y - SelectionStart.y);
					transform.position = new Vector3 (StartPosition.x + moveFactor.x, StartPosition.y, transform.position.z);
					int nearestLocator = 1;
					float minDist = Vector3.Distance(transform.position, locators[nearestLocator].transform.position);
					//i = 1 means skip main room
					Debug.Log(GameData.Instance.flipCol.GetCurrentRoom().enabled);
					for (int i = 1; i < locators.Count; i++) {
						float dist = Vector3.Distance (transform.position, locators [i].transform.position);
						if (dist < minDist) {
							nearestLocator = i;
							minDist = dist;
						}
					}
					GameData.MoveRoom (myType, nearestLocator);
						
				} else {
					amSelected = false;
				}
			}
		} else {
			GameObject myLocator = locators[GameData.GetPosition (myType)];
			float distanceToLocator = Vector3.Distance (transform.position, myLocator.transform.position);
			if (distanceToLocator < minSpeed * Time.deltaTime) {
				transform.position = myLocator.transform.position;
			} else {
				transform.position = Vector3.MoveTowards (transform.position, myLocator.transform.position, minSpeed * distanceToLocator * Time.deltaTime);
			}
		}

		if (Input.GetMouseButtonDown (0) && myType != RoomType.MAIN) {
			Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
			RaycastHit hit = new RaycastHit ();
			if(Physics.Raycast(ray, out hit, 1000f, RoomColliderMask))
			{
				if (hit.collider.gameObject == gameObject) {
					amSelected = true;
					Physics.Raycast (ray, out hit, 1000f, SliderColliderMask);
					SelectionStart = hit.point;
					StartPosition = transform.position;
				}
			}
		}
	}
}
