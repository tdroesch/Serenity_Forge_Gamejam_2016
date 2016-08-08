using UnityEngine;
using System.Collections;

public class FollowPointer : MonoBehaviour {

	StrokeDrawer strokeDrawer;
	// Use this for initialization
	void Start () {
		strokeDrawer = GetComponent<StrokeDrawer>();
	}
	
	// Update is called once per frame
	void Update () {
		RaycastHit hit;
		if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, float.MaxValue, LayerMask.GetMask("Canvas")))
		{
			transform.position = hit.point;
			if (Input.GetMouseButtonDown(0))
			{
				if (!strokeDrawer.isPaused)
				{
					strokeDrawer.NewStroke(); 
				}
			}
			if (Input.GetMouseButtonUp(0))
			{
				strokeDrawer.EndStroke();
			}
		} else
		{
			strokeDrawer.EndStroke();
		}
	}
}
