using UnityEngine;
using System.Collections;

[RequireComponent(typeof(AudioSource))]
public class AudioLibrary : MonoBehaviour {
	AudioSource source;
	public AudioClip[] audioClips;
	public string[] clipNames;
	public void PlayAudioClip(string name){
		for (int i = 0; i < clipNames.Length; i++) {
			if (clipNames[i].Equals (name)) {
				source.PlayOneShot (audioClips [i]);
			}
		}
	}
	// Use this for initialization
	void Start () {
		source = GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
