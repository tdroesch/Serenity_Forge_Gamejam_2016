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
		if (!sd.usingEraser)
		{
			switch (c)
			{
				case "red":
					sd.color = new Color(201, 31, 22);
					break;
				case "green":
					sd.color = new Color(14, 121, 19);
					break;
				case "blue":
					sd.color = new Color(16, 63, 132);
					break;
				case "yellow":
					sd.color = new Color(235, 208, 18);
					break;
				case "purple":
					sd.color = new Color(103, 16, 83);
					break;
				case "orange":
					sd.color = new Color(242, 152, 35);
					break;
				case "gray":
					sd.color = new Color(133, 129, 121);
					break;
				case "brown":
					sd.color = new Color(96, 65, 35);
					break;
				case "black":
					sd.color = new Color(17, 16, 14);
					break;
				case "pink":
					sd.color = new Color(248, 132, 147);
					break;
				default:
					sd.color = Color.gray;
					break;
			} 
		}
	}
}
