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

	void Awake()
	{
		Instance = this;
		MusicAudioSource.clip = Music;
	}

	void Update()
	{
		if (!MusicAudioSource.isPlaying)
			MusicAudioSource.Play ();
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
}
