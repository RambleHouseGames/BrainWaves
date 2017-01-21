using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomCollumn : MonoBehaviour {
	
	[SerializeField]
	private GameData gameData;

	[SerializeField]
	private List<Room> rooms;

	[SerializeField]
	private RoomType myType;

	[SerializeField]
	private float heightPerRoom = 9f;

	[SerializeField]
	private float minSpeed = 10f;

	[SerializeField]
	public Character character;

	void Update()
	{
		Vector3 target = new Vector3(transform.position.x, -(heightPerRoom * gameData.GetCurrentProgress()), transform.position.z);
		float dist = Vector3.Distance(transform.position, target);
		if (dist < minSpeed * Time.deltaTime) {
			transform.position = target;
			gameData.NotifyProgressAnimationComplete (myType);
		} else
			transform.position = Vector3.MoveTowards (transform.position, target, dist * Time.deltaTime * minSpeed);
	}

	public Room GetCurrentRoom()
	{
		return rooms[gameData.GetCurrentProgress()];
	}
}
