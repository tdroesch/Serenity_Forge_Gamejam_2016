using UnityEngine;
using System.Collections;

public class PageTracker : MonoBehaviour {

	int pageCount = 0;
	Page[] pages;
	public StrokeDrawer strokeDrawer;
	public BGManager bgManager;

	void OnEnable()
	{
		pages = FindObjectsOfType<Page>();
		if (pages.Length > 1)
		{
			pages[0].SetActive(true);
			pages[1].SetActive(false);
		}
		strokeDrawer = FindObjectOfType<StrokeDrawer>();
		bgManager = GetComponent<BGManager>();
	}

	public void NewPage()
	{
		if (!strokeDrawer.isPaused)
		{
			pages[pageCount % 2].Flip();
			pageCount++;
			pages[pageCount % 2].SetActive(true);
			if (pageCount < 50 && pageCount % 5 == 0)
			{
				bgManager.NextBackground();
			} 
		}
	}
}
