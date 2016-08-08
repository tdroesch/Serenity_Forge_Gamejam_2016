using UnityEngine;
using System.Collections;

public class BGAudio : MonoBehaviour {

	public AudioClip bgAudio;
	public bool loop;
	public Vector2 delayRange;
	[Range(0,1)]
	public float volume;
	bool playing;

	public void PlayAudio()
	{
		StartCoroutine(AudioLoop());
	}

	public void StopAudio()
	{
		playing = false;
	}

	IEnumerator AudioLoop()
	{
		playing = true;
		if (loop)
		{
			AudioSource audioTarget = Camera.main.gameObject.AddComponent<AudioSource>();
			audioTarget.clip = bgAudio;
			audioTarget.loop = true;
			audioTarget.volume = volume;
			audioTarget.Play();
			while (playing)
			{
				yield return null;
			}
			audioTarget.Stop();
			Destroy(audioTarget); 
		}
		else
		{
			AudioSource audioTarget = Camera.main.GetComponent<AudioSource>();
			audioTarget.PlayOneShot(bgAudio, volume);
			float delay = Random.Range(delayRange.x, delayRange.y);
			while (playing)
			{
				delay -= Time.deltaTime;
				if (delay <= 0)
				{
					audioTarget.PlayOneShot(bgAudio, volume/audioTarget.volume);
					delay = Random.Range(delayRange.x, delayRange.y);
				}
				yield return null;
			}
		}
	}
}
