using UnityEngine;
using System.Collections;

public class Background : MonoBehaviour {

	Vector3 startPosition;
	BGAudio bgAudio;

	// Use this for initialization
	void Awake () {
		startPosition = transform.position;
		bgAudio = GetComponent<BGAudio>();
	}

	public void MakeActive(Vector3 pos)
	{
		transform.position = pos;
		bgAudio.PlayAudio();
	}
	public void MakeInactive()
	{
		transform.position = startPosition;
		bgAudio.StopAudio();
	}
}
