using UnityEngine;
using UnityEngine.UI;
using GestureRecognizer;
using System.Collections;

public class HeartTracker : MonoBehaviour {

	int hearts = 0;
	public Text text;
	public Narrative narrative;

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
			narrative.PlayNextNarration(hearts);
			WriteHearts();
		}
	}

	void WriteHearts()
	{
		text.text = "Hearts Remaining: " + (40 - hearts).ToString();
	}
}
