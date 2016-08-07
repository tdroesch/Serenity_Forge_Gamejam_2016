﻿using UnityEngine;
using UnityEngine.UI;
using GestureRecognizer;
using System.Collections;

public class HeartTracker : MonoBehaviour {

	int hearts = 0;
	public Text text;

	void OnEnable()
	{
		GestureBehaviour.OnGestureRecognition += OnGestureRecognition;
	}

	void OnDisable()
	{
		GestureBehaviour.OnGestureRecognition -= OnGestureRecognition;
	}

	void OnDestroy()
	{
		GestureBehaviour.OnGestureRecognition -= OnGestureRecognition;
	}

	void OnGestureRecognition(Gesture g, Result r)
	{
		if (r.Score > 0.1f)
		{
			hearts++;
			WriteHearts();
		}
	}

	void WriteHearts()
	{
		text.text = hearts.ToString();
	}
}