using UnityEngine;
using System.Collections;

public class StrokeDrawer : MonoBehaviour {

	public float resolution = 20;
	public float storyResolution = 80;
	public float width = 0.2f;
	public float storyWidth = 0.05f;
	public Material strokeMaterial;
	public Transform strokeEnd;
	public Transform strokeContainer;
	private float startRotation;

	LineRenderer stroke;
	Transform startSprite;
	Transform endSprite;
	int points = 2;
	Vector3 lastPoint, nextPoint;
	bool storyMode;

	// Use this for initialization
	void Start () {
		startRotation = strokeEnd.rotation.eulerAngles.z;
	}
	
	// Update is called once per frame
	void Update () {
		if (stroke != null) {
			nextPoint = stroke.transform.InverseTransformPoint(transform.position);
			stroke.SetPosition(points-1,nextPoint);
			if (Vector3.Distance (lastPoint, nextPoint) > 0) {
				if (points == 2){
					startSprite.rotation = SpriteRotation(false);
				}
				endSprite.position = transform.position;
				endSprite.rotation = SpriteRotation(false);
			}
			float _resolution = storyMode?storyResolution:resolution;
			if (Vector3.Distance (lastPoint, nextPoint)>(1.0f/_resolution)){
				lastPoint = lastPoint + (nextPoint-lastPoint)/2;
				stroke.SetPosition(points-1, lastPoint);
				points++;
				stroke.SetVertexCount(points);
				stroke.SetPosition(points-1, nextPoint);
			}
		}
	}

	public GameObject NewStroke(bool _story){
		GameObject go = new GameObject ("Stroke");
		go.transform.parent = strokeContainer;
		go.transform.position = this.transform.position;
		startSprite = Instantiate (strokeEnd, transform.position, strokeEnd.transform.rotation) as Transform;
		if (_story){
			startSprite.localScale = new Vector3(storyWidth/width, storyWidth/width, 1);
		}
		startSprite.parent = go.transform;
		endSprite = Instantiate (strokeEnd, transform.position, strokeEnd.transform.rotation) as Transform;
		if (_story){
			endSprite.localScale = new Vector3(storyWidth/width, storyWidth/width, 1);
		}
		endSprite.parent = go.transform;
		stroke = go.AddComponent<LineRenderer>();
		stroke.material = strokeMaterial;
		if (_story){
			stroke.SetWidth (storyWidth, storyWidth);
		}
		else {
			stroke.SetWidth (width, width);
		}
		stroke.useWorldSpace = false;
		stroke.sortingOrder = 3;
		points = 2;
		lastPoint = new Vector3 ();
		nextPoint = new Vector3 ();
		stroke.SetVertexCount (points);
		stroke.SetPosition (0, lastPoint);
		stroke.SetPosition (1, nextPoint);
		storyMode = _story;
		return go;
	}

	public void EndStroke(){
		if (stroke!=null) {
			nextPoint = stroke.transform.InverseTransformPoint(transform.position);
			stroke.SetPosition(points-1,nextPoint);
			endSprite.position = transform.position;
			endSprite.rotation = SpriteRotation(false);
			stroke = null;
		}
	}

	Quaternion SpriteRotation(bool reverse){
		return Quaternion.Euler (0, 0, startRotation - Mathf.Sign((lastPoint - nextPoint).x) * Mathf.Rad2Deg *
		                  		Mathf.Acos(Vector3.Dot(Vector3.up, reverse?(nextPoint - lastPoint):(lastPoint - nextPoint)) /
		           		  		Vector3.Distance (nextPoint, lastPoint)));
	}
}
