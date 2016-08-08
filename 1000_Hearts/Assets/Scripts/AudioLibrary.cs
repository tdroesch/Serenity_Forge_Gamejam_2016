using UnityEngine;
using System.Collections;

[RequireComponent(typeof(AudioSource))]
public class AudioLibrary : MonoBehaviour {
	AudioSource source;
	public AudioClip[] audioClips;
	public string[] clipNames;
	private int loopingClip;
	public void PlayAudioClip(string name, float volume = 1){
		for (int i = 0; i < clipNames.Length; i++) {
			if (clipNames[i].Equals (name)) {
				source.PlayOneShot (audioClips [i], volume);
			}
		}
	}

	public void PlayAudioClipLoop(string name, float volume = 1)
	{
		for (int i = 0; i < clipNames.Length; i++)
		{
			if (clipNames[i].Equals(name))
			{
				StartCoroutine(LoopClipUntilStop(i, volume));
			}
		}
	}

	public void StopAudioClipLoop(string name)
	{
		for (int i = 0; i < clipNames.Length; i++)
		{
			if (clipNames[i].Equals(name) && i == loopingClip)
			{
				loopingClip = -1;
			}
		}
	}

	// Use this for initialization
	void Start () {
		source = GetComponent<AudioSource>();
	}
	
	IEnumerator LoopClipUntilStop(int i, float volume)
	{
		AudioSource clipLooper = gameObject.AddComponent<AudioSource>();
		clipLooper.loop = true;
		clipLooper.clip = audioClips[i];
		clipLooper.volume = volume;
		clipLooper.Play();
		loopingClip = i;
		while (loopingClip == i)
		{
			yield return null;
		}
		clipLooper.Stop();
		Destroy(clipLooper);
	}
}
