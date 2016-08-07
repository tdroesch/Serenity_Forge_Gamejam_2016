using UnityEngine;
using System.Collections;

public class ChangeStroke : MonoBehaviour {

	StrokeDrawer sd;
	public stroketype crayon;
	public stroketype pastel;
	public stroketype eraser;

	// Use this for initialization
	void OnEnable()
	{
		sd = GetComponent<StrokeDrawer>();
	}

	public void SetStroke(string s)
	{
		switch (s)
		{
			case "crayon":
				sd.strokeMaterial = crayon.strokeMat;
				sd.strokeEnd = crayon.endSprite;
				sd.strokeSound = crayon.strokeSound;
				if (sd.color == Color.white) sd.color = Color.red;
				break;
			case "pastel":
				sd.strokeMaterial = pastel.strokeMat;
				sd.strokeEnd = pastel.endSprite;
				sd.strokeSound = pastel.strokeSound;
				if (sd.color == Color.white) sd.color = Color.red;
				break;
			case "eraser":
				sd.strokeMaterial = eraser.strokeMat;
				sd.strokeEnd = eraser.endSprite;
				sd.strokeSound = eraser.strokeSound;
				sd.color = Color.white;
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
	}
}
