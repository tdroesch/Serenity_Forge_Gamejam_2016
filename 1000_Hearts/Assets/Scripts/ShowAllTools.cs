using UnityEngine;
using System.Collections.Generic;

public class ShowAllTools : MonoBehaviour {

	public List<MeshRenderer> tools;

	public void Show()
	{
		foreach (MeshRenderer mr in tools)
		{
			mr.enabled = true;
		}
	}
}
