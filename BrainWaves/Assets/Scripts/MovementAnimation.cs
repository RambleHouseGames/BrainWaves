using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class MovementAnimation {

	public const float SlideTime = .12f;

	private static int animating = 0;

	public static void StartAnimation() {
		animating += 1;
	}

	public static void EndAnimation() {
		animating -= 1;
		if (animating == 0) DoneAnimating();
	}

	public static bool Animating {
		get { return animating > 0; }
	}

	public static event System.Action DoneAnimating;

	public static IEnumerator SlideTo(Transform transform, Vector3 moveTo, float time = SlideTime) {
		StartAnimation();
		Vector3 moveFrom = transform.position;
		transform.position = moveFrom;
		float t = 0f;
		while (t < time) {
			var x = t / time;
			var p = Vector3.Lerp(moveFrom, moveTo, x);
			transform.position = p;
			t += Time.deltaTime;
			yield return 0;
		}
		transform.position = moveTo;
		EndAnimation();
	}
}
