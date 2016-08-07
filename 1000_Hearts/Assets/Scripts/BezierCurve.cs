using UnityEngine;
using System.Collections;

[RequireComponent(typeof(LineRenderer))]
[ExecuteInEditMode]
public class BezierCurve : MonoBehaviour {

	[SerializeField]
	private Vector3[] points;
	public int lineQuality = 30;

	public int ControlPointCount {
		get {
			return points.Length;
		}
	}
	
	public Vector3 GetControlPoint (int index) {
		return points[index];
	}
	
	public void SetControlPoint (int index, Vector3 point) {
		if (index % 3 == 0) {
			Vector3 delta = point - points[index];
			if (index > 0) {
				points[index - 1] += delta;
			}
			if (index + 1 < points.Length) {
				points[index + 1] += delta;
			}
		}
		points[index] = point;
		UpdateLineRenderer();
	}

	public Vector3 GetPoint (float t) {
		return transform.TransformPoint(Bezier.GetPoint(points[0], points[1], points[2], points[3], t));
	}
	
	public Vector3 GetVelocity (float t) {
		return transform.TransformPoint(
			Bezier.GetFirstDerivative(points[0], points[1], points[2], points[3], t)) - transform.position;
	}

	public void Reset(){
		points = new Vector3[]{
			new Vector3(1f,0f,0f),
			new Vector3(2f,0f,0f),
			new Vector3(3f,0f,0f),
			new Vector3(4f,0f,0f)};
	}

	void UpdateLineRenderer () {
		LineRenderer lr = GetComponent<LineRenderer> () as LineRenderer;
		lr.SetVertexCount (lineQuality+1);
		for (int i=0; i<=lineQuality; i++){
			lr.SetPosition(i, transform.InverseTransformPoint(this.GetPoint((float)i/lineQuality)));
		}
	}

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
