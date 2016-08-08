using UnityEngine;
using System.Collections;

public class ToolSelect : MonoBehaviour {

	public ChangeStroke changeStroke;
	public string stroke;
	public ShowAllTools sat;
	
	void OnMouseDown()
	{
		changeStroke.SetStroke(stroke);
		sat.Show();
		GetComponent<MeshRenderer>().enabled = false;
	}
}
