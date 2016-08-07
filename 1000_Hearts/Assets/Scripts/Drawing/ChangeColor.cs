using UnityEngine;
using System.Collections;

public class ChangeColor : MonoBehaviour {

	StrokeDrawer sd;
	// Use this for initialization
	void OnEnable() {
		sd = GetComponent<StrokeDrawer>();
	}

	public void SetColor(string c)
	{
		switch (c)
		{
			case "red":
				sd.color = Color.red;
				break;
			case "green":
				sd.color = Color.green;
				break;
			case "blue":
				sd.color = Color.blue;
				break;
			default:
				sd.color = Color.gray;
				break;
		}
	}
}
