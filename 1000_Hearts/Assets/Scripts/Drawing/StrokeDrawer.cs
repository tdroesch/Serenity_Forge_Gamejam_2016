using UnityEngine;
using Line2D;
using System.Collections;

public class StrokeDrawer : MonoBehaviour {

	public Color color = Color.red;
	public float resolution = 40;
	public float width = 0.2f;
	public Material strokeMaterial;
	public Transform strokeEnd;
	public Transform strokeContainer;
	public string strokeSound;
	private AudioLibrary audioLibrary;
	private float startRotation;

	Line2DRenderer stroke;
	Transform startSprite;
	Transform endSprite;
	int points = 2;
	Vector3 lastPoint, nextPoint;
	int linecount = 0;

	// Use this for initialization
	void Start () {
		startRotation = strokeEnd.rotation.eulerAngles.z;
		audioLibrary = Camera.main.GetComponent<AudioLibrary>();
	}
	
	// Update is called once per frame
	void Update () {
		if (stroke != null) {
			nextPoint = stroke.transform.InverseTransformPoint(transform.position);
			stroke.points[points-1].pos = nextPoint;
			if (Vector3.Distance (lastPoint, nextPoint) > 0) {
				if (points == 2){
					if (startSprite != null)
					{
						startSprite.rotation = SpriteRotation(false); 
					}
				}
				if (endSprite != null)
				{
					endSprite.position = transform.position;
					endSprite.rotation = SpriteRotation(false); 
				}
			}
			if (Vector3.Distance (lastPoint, nextPoint)>(1.0f/resolution)){
				lastPoint = lastPoint + (nextPoint-lastPoint)/2;
				stroke.points[points - 1].pos = lastPoint;
				points++;
				stroke.points.Add(new Line2DPoint(nextPoint, 1, Color.white));
			}
		}
	}

	public GameObject NewStroke(){
		linecount++;
		GameObject go = new GameObject ("Stroke");
		go.transform.parent = strokeContainer;
		go.transform.position = this.transform.position;
		startSprite = NewStrokeEnd(go);
		endSprite = NewStrokeEnd(go);
		stroke = go.AddComponent<Line2DRenderer>();
		stroke.meshRenderer.material = strokeMaterial;
		stroke.widthMultiplier = width;
		stroke.useWorldSpace = false;
		//stroke.tilingU = 0.5f;
		stroke.meshRenderer.sortingLayerName = "Drawing";
		stroke.meshRenderer.sortingOrder = linecount;
		stroke.colorTint = color;
		points = 2;
		lastPoint = new Vector3 ();
		nextPoint = new Vector3 ();
		stroke.points.Add(new Line2DPoint(lastPoint, 1, Color.white));
		stroke.points.Add(new Line2DPoint(nextPoint, 1, Color.white));
		PlayStroke();
		return go;
	}

	public void EndStroke(){
		if (stroke!=null)
		{
			if (points == 2 && startSprite != null)
			{
				startSprite.parent = strokeContainer;
				Destroy(stroke.gameObject);
			} else
			{
				nextPoint = stroke.transform.InverseTransformPoint(transform.position);
				stroke.points[points - 1].pos = nextPoint;
				if (endSprite != null)
				{
					endSprite.position = transform.position;
					endSprite.rotation = SpriteRotation(false); 
				}
			}
			stroke = null;
		}
	}

	Quaternion SpriteRotation(bool reverse){
		return Quaternion.Euler (0, 0, startRotation - Mathf.Sign((lastPoint - nextPoint).x) * Mathf.Rad2Deg *
		                  		Mathf.Acos(Vector3.Dot(Vector3.up, reverse?(nextPoint - lastPoint):(lastPoint - nextPoint)) /
		           		  		Vector3.Distance (nextPoint, lastPoint)));
	}

	Transform NewStrokeEnd(GameObject go)
	{
		if (strokeEnd != null)
		{
			Transform newSprite = Instantiate(strokeEnd, transform.position, strokeEnd.transform.rotation) as Transform;
			newSprite.parent = go.transform;
			newSprite.transform.localScale = new Vector3(width, width, width);
			SpriteRenderer spriteRenderer = newSprite.GetComponent<SpriteRenderer>();
			spriteRenderer.color = color;
			spriteRenderer.sortingOrder = linecount;
			return newSprite;
		}
		else return null;
	}

	void PlayStroke()
	{
		audioLibrary.PlayAudioClip(strokeSound);
	}
}
