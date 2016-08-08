using UnityEngine;
using System.Collections;

public class ChangeStroke : MonoBehaviour {

	StrokeDrawer sd;
	AudioLibrary al;
	public stroketype crayon;
	public stroketype pastel;
	public stroketype eraser;

	// Use this for initialization
	void OnEnable()
	{
		sd = GetComponent<StrokeDrawer>();
		al = Camera.main.GetComponent<AudioLibrary>();
	}

	public void SetStroke(string s)
	{
		switch (s)
		{
			case "crayon":
				sd.strokeMaterial = crayon.strokeMat;
				sd.strokeEnd = crayon.endSprite;
				sd.strokeSound = crayon.strokeSound;
				sd.volume = crayon.useVolume;
				if (sd.color == Color.white) sd.color = Color.red;
				sd.usingEraser = false;
				al.PlayAudioClip("selectcrayon", crayon.selectVolume);
				break;
			case "pastel":
				sd.strokeMaterial = pastel.strokeMat;
				sd.strokeEnd = pastel.endSprite;
				sd.strokeSound = pastel.strokeSound;
				sd.volume = pastel.useVolume;
				if (sd.color == Color.white) sd.color = Color.red;
				sd.usingEraser = false;
				al.PlayAudioClip("selectpastel", pastel.selectVolume);
				break;
			case "eraser":
				sd.strokeMaterial = eraser.strokeMat;
				sd.strokeEnd = eraser.endSprite;
				sd.strokeSound = eraser.strokeSound;
				sd.volume = eraser.useVolume;
				sd.color = Color.white;
				sd.usingEraser = true;
				al.PlayAudioClip("selecteraser", eraser.selectVolume);
				break;
			default:
				break;
		}
	}

	[System.Serializable]
	public struct stroketype
	{
		public Material strokeMat;
		public Transform endSprite;
		public string strokeSound;
		public float selectVolume;
		public float useVolume;
	}
}
