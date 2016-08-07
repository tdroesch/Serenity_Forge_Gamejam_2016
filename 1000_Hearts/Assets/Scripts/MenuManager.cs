using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using System.Collections.Generic;

public class MenuManager : MonoBehaviour {
	public List<BezierCurve> strokeOrder;
	public List<float> strokeTime;
	public List<BrushTip.Direction> strokeDirection;
	private int currentCurve = -1;
	public CurveTracer tracer;
	public BrushTip brush;
	public Animator characterAnimator;
	private bool readyToTrace;
	public string firstLevel;
	private GameObject lastStroke;
	
	private void NextStroke ()
	{
		tracer.SetAtStart (strokeOrder [currentCurve], strokeTime [currentCurve]);
		if (brush.SetAtStart (strokeDirection [currentCurve], 0, SetReady)){
			LoadLevel (firstLevel);
		} else {
			if (lastStroke != null) {
				Destroy(lastStroke);
			}
		}
	}

	public void LoadLevel(string levelname){
		Application.LoadLevel (levelname);
	}

	public void StartTrace () {
		if (currentCurve > -1)
			return;
		currentCurve++;
		Cursor.visible = false;
		strokeOrder[currentCurve].gameObject.SetActive(true);
		if (tracer != null && brush != null){
			tracer.SetAtStart(strokeOrder[currentCurve], strokeTime[currentCurve]);
			brush.CharacterAnimator = characterAnimator;
			brush.SetAtStart (strokeDirection[currentCurve], 0, SetReady);
		}
	}

	private void SetReady(){
		readyToTrace = true;
	}

	// Use this for initialization
	void Start () {
		if (strokeOrder.Count > 0) {
			if (strokeTime.Count < strokeOrder.Count){
				int difference = strokeOrder.Count-strokeTime.Count;
				for (int i=0; i<difference; i++){
					strokeTime.Add(1);
				}
			}
			if (strokeDirection.Count < strokeOrder.Count){
				int difference = strokeOrder.Count-strokeDirection.Count;
				for (int i=0; i<difference; i++){
					strokeDirection.Add(BrushTip.Direction.vertical);
				}
			}
		}
	}

	
	// Update is called once per frame
	void Update () {
		if (Input.GetMouseButtonDown(0) && readyToTrace){
			tracer.Trace(NextStroke);
			lastStroke = brush.NewStroke (false);
			readyToTrace = false;
		}
	}

	void OnDrawGizmos(){
		Gizmos.DrawIcon(transform.position, "LevelEditor.png");
	}
}
