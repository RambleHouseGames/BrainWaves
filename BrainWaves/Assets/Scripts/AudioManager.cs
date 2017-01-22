using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour 
{
	public static AudioManager Instance;

	[SerializeField]
	private AudioClip OpenDoor;

	[SerializeField]
	private AudioClip CloseDoor;

	[SerializeField]
	private AudioClip Swish;

	[SerializeField]
	private AudioClip Music;

	[SerializeField]
	private AudioClip Rumble;

	[SerializeField]
	private AudioSource EffectAudioSource;

	[SerializeField]
	private AudioSource MusicAudioSource;

	[SerializeField]
	private AudioSource RumbleAudioSource;

	private float randomRumbleGap;

	void Awake()
	{
		Instance = this;
		MusicAudioSource.clip = Music;
		randomRumbleGap = Random.Range (3f, 10f);
	}

	void Update()
	{
		if (!MusicAudioSource.isPlaying)
			MusicAudioSource.Play ();

		randomRumbleGap -= Time.deltaTime;
		if (randomRumbleGap <= 0)
			AudioManager.Instance.PlayRumble ();
	}

	public void PlayOpenDoor()
	{
		EffectAudioSource.Stop ();
		EffectAudioSource.clip = OpenDoor;
		EffectAudioSource.Play ();
	}

	public void PlayCloseDoor()
	{
		EffectAudioSource.Stop ();
		EffectAudioSource.clip = CloseDoor;
		EffectAudioSource.Play ();
	}

	public void PlaySwish()
	{
		EffectAudioSource.Stop ();
		EffectAudioSource.clip = Swish;
		EffectAudioSource.Play ();
	}

	public void PlayRumble()
	{
		if (!RumbleAudioSource.isPlaying)
			RumbleAudioSource.Play ();
	}
}
