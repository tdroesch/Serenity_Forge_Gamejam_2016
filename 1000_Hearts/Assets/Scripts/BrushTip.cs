using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(StrokeDrawer))]
public class BrushTip : MonoBehaviour {
	public CurveTracer tracer;
	public TraceType traceFunction = TraceType.directional;
	public Direction traceDirection;
//	public SpriteRenderer handRenderer;
//	public Sprite[] handSprites;
	public float sensetivity = 0.03f;
	public float threshold = 0.1f;
	public float accuracyScale = 4;

	private float totalDif = 0;
	private int readings = 0;
	private Animator characterAnimator;
	private int badStrokes = 0;
	public int strokesToFail = 3;

	private bool automatic = false;

	private StrokeDrawer strokeDrawer;

	public float AverageDistance{
		get {
			return readings > 0 ? totalDif/readings: 0;
		}
	}
	public float Distance{
		get {
			return readings > 0 ? characterAnimator.GetFloat ("Accuracy"): 0;
		}
	}
	public int Honor{
		get {
			return Mathf.RoundToInt((0.3f - AverageDistance) * 10000.0f/3.0f);
		}
	}

	public Animator CharacterAnimator{
		set { characterAnimator = value;}
	}
	
	public enum Direction{
		vertical, horizontal
	}
	public enum TraceType{
		directional, bidirectional, free
	}
	
	public delegate void CallBack();

	public bool SetAtStart (Direction strokeDirection, float delay, CallBack callback)
	{
		strokeDrawer.EndStroke ();
		automatic = false;
		StartCoroutine(MoveToStart(tracer.transform.localPosition, delay, callback));
		traceDirection = strokeDirection;
		if (characterAnimator != null) {
			if (characterAnimator.GetFloat ("Accuracy") > 1) {
				characterAnimator.SetBool ("Good", false);
				badStrokes++;
			} else {
				characterAnimator.SetBool ("Good", true);
			}
			characterAnimator.SetFloat ("Accuracy", 0);
		}
		//		handRenderer.sprite = handSprites [0];
		if (badStrokes >= strokesToFail) {
			characterAnimator.SetTrigger ("Execute");
			badStrokes = 0;
			return false;
		} else {
			return true;
		}
	}

	private IEnumerator MoveToStart(Vector3 target, float delay, CallBack callback){
		yield return new WaitForSeconds(delay);
		while (transform.localPosition != target) {
			transform.localPosition = Vector3.Lerp (transform.localPosition, target,
			                                        3 * Time.deltaTime / Vector3.Distance (transform.localPosition, target));
			yield return null;
		}
		callback();
	}

	private Vector3 GetPositionDirectional(){
		Vector3 position = transform.localPosition;
		if (traceDirection == Direction.vertical) {
			position.y = tracer.transform.localPosition.y;
			position.x += sensetivity * Input.GetAxis ("Mouse X");
		} else {
			position.x = tracer.transform.localPosition.x;
			position.y += sensetivity * Input.GetAxis ("Mouse Y");
		}
		return position;
	}

	private Vector3 GetPositionBiDirectional(){
		Vector3 position = transform.localPosition;
		position.x += sensetivity * Input.GetAxis ("Mouse X");
		position.y += sensetivity * Input.GetAxis ("Mouse Y");
		return position;
	}

	private Vector3 GetPositionFree(){
		Vector3 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		pos.z = 0;
		return pos;
	}

	public GameObject NewStroke(bool _automatic){
		automatic = _automatic;
		return strokeDrawer.NewStroke (_automatic);
//		HandPosition ();
	}

	public void ResetBrush(){
		badStrokes = 0;
		totalDif = 0;
		readings = 0;
		characterAnimator.SetFloat("Accuracy", 0);
	}

//	private void HandPosition(){
//		switch (traceDirection) {
//		case Direction.horizontal:
//			if (tracer.curve.GetControlPoint (0).x > tracer.curve.GetControlPoint (1).x) {
//				handRenderer.sprite = handSprites [1];
//			} else {
//				handRenderer.sprite = handSprites [2];
//			}
//			break;
//		case Direction.vertical:
//			if (tracer.curve.GetControlPoint (0).y > tracer.curve.GetControlPoint (1).y) {
//				handRenderer.sprite = handSprites [3];
//			} else {
//				handRenderer.sprite = handSprites [4];
//			}
//			break;
//		}
//	}

	// Use this for initialization
	void Awake () {
		strokeDrawer = GetComponent<StrokeDrawer> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (tracer.Tracing) {
			Cursor.lockState = CursorLockMode.Locked;
			if (automatic){
				transform.localPosition = tracer.transform.localPosition;
			}
			else {
				switch (traceFunction){
				case TraceType.directional:
					transform.localPosition = GetPositionDirectional();
					break;
				case TraceType.bidirectional:
					transform.localPosition = GetPositionBiDirectional();
					break;
				case TraceType.free:
					transform.localPosition = GetPositionFree();
					break;
				}
				float distance = Vector3.Distance (transform.localPosition, tracer.transform.localPosition);
				totalDif += distance;
				readings++;
				if (characterAnimator != null) {
					characterAnimator.SetFloat("Accuracy", characterAnimator.GetFloat ("Accuracy")+distance-threshold);
				}
			}
		}
	}
}
