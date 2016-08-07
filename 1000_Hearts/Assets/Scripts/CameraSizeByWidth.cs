using UnityEngine;
using System.Collections;

public class CameraSizeByWidth : MonoBehaviour {
	private Camera mainCamera;
	private float width;
	// Use this for initialization
	void Start () {
		mainCamera = GetComponent<Camera>();
		width = mainCamera.orthographicSize * 2 * (16.0f/9.0f);
	}
	
	// Update is called once per frame
	void Update () {
		mainCamera.orthographicSize = width / (mainCamera.aspect * 2);
	}
}
