using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class Narrative : MonoBehaviour {

	public List<NarrativeElement> narration;
	int currentElement = 0;
	public Text textTarget;
	public Text EndGameText;
	//public AudioSource audioTarget;

	public void PlayNextNarration(int hearts)
	{
		if (hearts >= narration[currentElement].hearts)
		{
			textTarget.text = narration[currentElement].textNarration;
			//audioTarget.PlayOneShot(narration[currentElement].audioNarration);
			currentElement++;
			if (currentElement >= narration.Count)
			{
				EndGameText.enabled = true;
				FindObjectOfType<StrokeDrawer>().isPaused = true; 
			}
		}
	}

	[System.Serializable]
	public struct NarrativeElement
	{
		public int hearts;
		public string textNarration;
		//public AudioClip audioNarration;
	}
}
