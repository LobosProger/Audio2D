using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Audio2D : MonoBehaviour
{
	//* This needs, if we assigning global listener component
	////* 
	////* Because, if more easier operation, than FindGameobjectOfType
	[SerializeField]
	private bool assignAudioListener;
	public static AudioListener listener;

	//* Every object can has different value settings, so we just keep their in variable
	private float attemptedVolumeInStart;
	private AudioSource source;

	//* Keep time, because we need change volume on not every frame (for optimization)
	private double lastTime;

	//* We assigning global audio listener? If yes - just assign to variable listener Audio listener component
	////* If no - get component audio source to make manipulations with its
	void Awake()
	{
		if (assignAudioListener)
			listener = GetComponent<AudioListener>();
		else
		{
			source = gameObject.GetComponent<AudioSource>();

			////* => Every object can has different value settings, so we just keep their in variable
			attemptedVolumeInStart = source.volume;
			source.volume = 0;
		}
	}

	//* Just make operations in Update, checking last time and calling function
	//! We don't using IEnumerators, because they require allocations
	private void Update()
	{
		if (!assignAudioListener && Time.fixedTime >= lastTime + 0.083f)
		{
			lastTime = Time.fixedTime;
			ChangeAudioVolume();
		}
	}

	//* Just calculating volume
	private void ChangeAudioVolume()
	{
		float distance = (transform.position - listener.transform.position).sqrMagnitude;
		source.volume = Mathf.Lerp(0, 1 * attemptedVolumeInStart, Mathf.InverseLerp(source.maxDistance * source.maxDistance, source.minDistance * source.minDistance, distance));
	}
}
