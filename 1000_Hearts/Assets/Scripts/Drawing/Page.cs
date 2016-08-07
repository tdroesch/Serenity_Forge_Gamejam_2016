using UnityEngine;
using System.Collections;

public class Page : MonoBehaviour {

	public Vector3 pageAchor;
	public Vector3 pageAxis;
	public float flipAngle;
	private StrokeDrawer strokeDrawer;

	void Awake()
	{
		strokeDrawer = FindObjectOfType<StrokeDrawer>();
		if(strokeDrawer == null)
		{
			Debug.Log("No stroke drawer found.");
		}
	}

	public void Flip()
	{
		StartCoroutine(PageFlip());
	}

	public void SetActive(bool active)
	{
		if (active)
		{
			//gameObject.layer = LayerMask.NameToLayer("Canvas");
			GetComponent<MeshRenderer>().sortingOrder = 1;
			strokeDrawer.strokeContainer = transform;
			//GameObject backing = GameObject.CreatePrimitive(PrimitiveType.Quad);
			//backing.transform.parent = transform;
			//backing.transform.localScale = new Vector3(1.1f, 1.1f, 1.1f);
			//backing.transform.localPosition = new Vector3();
			//backing.GetComponent<MeshRenderer>().sortingLayerName ="CanvasBack";
			//backing.GetComponent<MeshRenderer>().sortingOrder = -1;
		}
		else
		{
			//gameObject.layer = LayerMask.NameToLayer("BackCanvas");
			GetComponent<MeshRenderer>().sortingOrder = -1;
			for (int i = 0; i < transform.childCount; i++)
			{
				Destroy(transform.GetChild(i).gameObject);
			}
		}
	}

	IEnumerator PageFlip()
	{
		Vector3 resetPos = transform.position;
		Quaternion resetRot = transform.rotation;
		bool flipped = false;
		float rotation = 0;
		for (float time = 0; time < 2; time += Time.deltaTime)
		{
			float deltaR = 180 * Time.deltaTime / 2;
			transform.RotateAround(pageAchor, pageAxis, deltaR);
			rotation += deltaR;
			if (!flipped && rotation > flipAngle)
			{
				flipped = true;
				SetActive(false);
			}
			yield return null;
		}
		transform.position = resetPos;
		transform.rotation = resetRot;
	}
}
