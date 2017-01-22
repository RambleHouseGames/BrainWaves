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

	public static IEnumerator SlideTo(Transform transform, Vector3 moveTo, float delay = 0f, float time = SlideTime) {
		StartAnimation();
		
		// Delay wait.
		float t = 0f;
		while (t < delay) {
			t += Time.deltaTime;
			yield return 0;
		}

		// Offset delay time for next count-down.
		t -= delay;

		// Animate.
		Vector3 moveFrom = transform.position;
		transform.position = moveFrom;
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

	public static IEnumerator DelayFor(float delay, System.Action action) {
		StartAnimation();
		float t = 0f;
		while (t < delay) {
			t += Time.deltaTime;
			yield return 0;
		}
		action();
		EndAnimation();
	}
}
