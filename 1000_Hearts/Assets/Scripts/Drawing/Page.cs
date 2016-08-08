using UnityEngine;
using System.Collections;

public class Page : MonoBehaviour {

	public Vector3 pageAchor;
	public Vector3 pageAxis;
	public float flipAngle;
	private StrokeDrawer strokeDrawer;
	private AudioLibrary audioLibrary;
	public float volume;

	void Awake()
	{
		strokeDrawer = FindObjectOfType<StrokeDrawer>();
		if(strokeDrawer == null)
		{
			Debug.Log("No stroke drawer found.");
		}
		audioLibrary = Camera.main.GetComponent<AudioLibrary>();
	}

	public void Flip()
	{
		StartCoroutine(PageFlip());
	}

	public void SetActive(bool active)
	{
		if (active)
		{
			GetComponent<MeshRenderer>().sortingOrder = 1;
			strokeDrawer.strokeContainer = transform;
		}
		else
		{
			GetComponent<MeshRenderer>().sortingOrder = -1;
			for (int i = 0; i < transform.childCount; i++)
			{
				Destroy(transform.GetChild(i).gameObject);
			}
		}
	}

	IEnumerator PageFlip()
	{
		strokeDrawer.isPaused = true;
		Vector3 resetPos = transform.position;
		Quaternion resetRot = transform.rotation;
		bool flipped = false;
		float rotation = 0;
		for (float time = 0; time < 2; time += Time.deltaTime)
		{
			float deltaR = 180 * Time.deltaTime / 2;
			transform.RotateAround(pageAchor, pageAxis, deltaR);
			rotation += deltaR;
			if (!flipped && rotation > flipAngle)
			{
				flipped = true;
				SetActive(false);
				audioLibrary.PlayAudioClip("turnpage", volume);
			}
			yield return null;
		}
		transform.position = resetPos;
		transform.rotation = resetRot;
		strokeDrawer.isPaused = false;
	}
}
