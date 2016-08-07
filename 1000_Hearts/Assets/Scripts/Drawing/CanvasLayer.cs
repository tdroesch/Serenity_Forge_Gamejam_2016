using UnityEngine;
using System.Collections;

public class CanvasLayer : MonoBehaviour {

	// Use this for initialization
	void Awake () {
		GetComponent<MeshRenderer>().sortingLayerName = "Canvas";
	}
}
