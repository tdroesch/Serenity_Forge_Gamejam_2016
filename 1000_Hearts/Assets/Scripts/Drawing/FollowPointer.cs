using UnityEngine;
using System.Collections;

public class FollowPointer : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		RaycastHit hit;
		if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, float.MaxValue, LayerMask.GetMask("Canvas")))
		{
			transform.position = hit.point;
			if (Input.GetMouseButtonDown(0))
			{
				GetComponent<StrokeDrawer>().NewStroke();
			}
			if (Input.GetMouseButtonUp(0))
			{
				GetComponent<StrokeDrawer>().EndStroke();
			}
		} else
		{
			GetComponent<StrokeDrawer>().EndStroke();
		}
	}
}
