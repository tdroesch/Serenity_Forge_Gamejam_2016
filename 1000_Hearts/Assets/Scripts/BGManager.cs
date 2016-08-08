using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System;

public class BGManager : MonoBehaviour {

	public Image transitionScreen;
	public List<Background> backgrounds;
	public Vector3 bgPosition;
	int bgIndex = 0;
	StrokeDrawer strokeDrawer;

	void Start()
	{
		backgrounds[0].MakeActive(bgPosition);
		strokeDrawer = FindObjectOfType<StrokeDrawer>();
	}

	public void NextBackground()
	{
		StartCoroutine(Transition());
	}

	private IEnumerator Transition()
	{
		strokeDrawer.isPaused = true;
		while(transitionScreen.color.a < 1)
		{
			transitionScreen.color = new Color(0,0,0, transitionScreen.color.a + Time.deltaTime);
			yield return null;
		}
		ChangeToNextBG();
		while (transitionScreen.color.a > 0)
		{
			transitionScreen.color = new Color(0, 0, 0, transitionScreen.color.a - Time.deltaTime);
			yield return null;
		}
		strokeDrawer.isPaused = false;
	}

	void ChangeToNextBG()
	{
		backgrounds[bgIndex].MakeInactive();
		bgIndex++;
		backgrounds[bgIndex].MakeActive(bgPosition);
	}
}
