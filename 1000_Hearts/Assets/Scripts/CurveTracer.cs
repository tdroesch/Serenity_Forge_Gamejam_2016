using UnityEngine;
using System.Collections;

public class CurveTracer : MonoBehaviour {
	public BezierCurve curve;

	public float duration;

	private float progress;

	private bool tracing = false;

	public bool Tracing {
		get {
			return tracing;
		}
	}
	
	public delegate void CallBack();

	private CallBack EndTrace;

	public void SetAtStart (BezierCurve c, float i)
	{
		curve = c;
		duration = i;
		progress = 0;
		transform.localPosition = curve.GetPoint (progress);
	}

	public void Trace(CallBack callBack){
		tracing = true;
		EndTrace = callBack;
	}

	private void OnTraceEnd(){
		tracing = false;
		EndTrace();
	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (tracing) {
			if (progress > 1f) {
				OnTraceEnd();
			}
			else {
				progress += Time.deltaTime / duration;
				transform.localPosition = curve.GetPoint (progress);
			}
		}

		//Vector3 mousepos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		//mousepos.z = 0;
		//Debug.Log ("Distance from Tracer: " + Vector3.Distance(transform.position, mousepos));
	}
}
