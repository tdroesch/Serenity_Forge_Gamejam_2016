using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class LevelManager : MonoBehaviour {
	public List<Stroke> strokes;
	private List<GameObject> levelStrokes;
	public int totalStoryCurves = 94;
	public float postStoryDelay = 5.0f;
	private int currentCurve = 0;
	public CurveTracer tracer;
	public BrushTip brush;
	public Animator characterAnimator;
	public MaskableGraphic[] finalUI;
	private float[] finalUIAlpha;
	public float fadeTime = 5.0f;
	public Text score;
	private bool fadeIn = false;
	private SpriteRenderer handRender;
	private bool readyToTrace;
	
	private void NextStroke ()
	{
//		currentCurve++;
//		if (currentCurve < strokeOrder.Count) {
//			tracer.SetAtStart (strokeOrder [currentCurve], strokeTime [currentCurve]);
//			if (!brush.SetAtStart (strokeDirection [currentCurve], SetReady)){
//				currentCurve = strokeOrder.Count;
//				EndGame (false);
//			}
//		} else {
//			EndGame(true);
//		}
		currentCurve++;
		float delay = 0.0f;
		if (currentCurve == totalStoryCurves){
			delay = postStoryDelay;
		} 
		if (currentCurve < strokes.Count) {
			tracer.SetAtStart (strokes [currentCurve].curve, strokes [currentCurve].duration);
			if (!brush.SetAtStart (strokes [currentCurve].direction, delay, SetReady)){
				currentCurve = strokes.Count;
				EndGame (false);
			}
		} else {
			EndGame(true);
		}
	}

	private void StartTrace (bool automatic){
		tracer.Trace(NextStroke);
		GameObject stroke = brush.NewStroke (automatic);
		if (currentCurve >= totalStoryCurves){
			levelStrokes.Add(stroke);
		}
		if (!automatic) {
			characterAnimator.SetTrigger ("Next Stroke");
		}
		readyToTrace = false;
	}

	private void EndGame(bool success){
		Cursor.lockState = CursorLockMode.None;
		Cursor.visible = true;
		fadeIn = true;
		score.text += brush.Honor;
		for (int i = 0; i < finalUI.Length; i++){
			if (i >= 2){
				finalUI[i].enabled = true;
			} else if(success){
				finalUI[i].enabled = true;
			}
		}
	}
	
	public void LoadLevel(string levelname){
		Application.LoadLevel (levelname);
	}

	private void SetReady(){
		if (currentCurve < strokes.Count) {
			readyToTrace = true;
			if (strokes[currentCurve].automatic){
				StartTrace(true);
			}
		}
	}

	public void ResetLevel(){
		Cursor.visible = false;
		fadeIn = false;
		characterAnimator.SetTrigger ("Next Stroke");
		currentCurve = totalStoryCurves-1;
		NextStroke();
	}

	private void ResetFinalUI(){
		score.text = "Honor: ";
		for (int i = 0; i < finalUI.Length; i++){
			finalUI[i].enabled = false;
		}
		foreach (GameObject go in levelStrokes){
			Destroy(go);
		}
		levelStrokes = new List<GameObject>();
	}

	// Use this for initialization
	void Start () {
		if (strokes.Count > 0) {
			if (tracer != null && brush != null){
				tracer.SetAtStart(strokes[currentCurve].curve, strokes[currentCurve].duration);
				brush.CharacterAnimator = characterAnimator;
				brush.SetAtStart (strokes[currentCurve].direction, 0, SetReady);
			}
		}
		levelStrokes = new List<GameObject>();
		handRender = brush.gameObject.GetComponentInChildren<SpriteRenderer> ();
		Cursor.visible = false;
		finalUIAlpha = new float[finalUI.Length];
		for (int i = 0; i < finalUI.Length; i++){
			finalUIAlpha[i] = finalUI[i].color.a;
			Color zeroAlpha = finalUI[i].color;
			zeroAlpha.a = 0;
			finalUI[i].color = zeroAlpha;
			finalUI[i].enabled = false;
		}
	}

	
	// Update is called once per frame
	void Update () {
		if (Input.GetMouseButtonDown(0) && readyToTrace){
			StartTrace(false);
		}
		if (fadeIn && finalUI[0].color.a < finalUIAlpha[0]) {
			for (int i = 0; i < finalUI.Length; i++) {
				Color newColor = finalUI [i].color;
				newColor.a += Time.deltaTime * finalUIAlpha [i] / fadeTime;
				finalUI [i].color = newColor;
			}
			Color newHandColor = handRender.color;
			newHandColor.a -= Time.deltaTime / fadeTime;
			handRender.color = newHandColor;
		}
		if (!fadeIn && finalUI[0].color.a > 0) {
			for (int i = 0; i < finalUI.Length; i++) {
				Color newColor = finalUI [i].color;
				newColor.a -= Time.deltaTime * finalUIAlpha [i] / fadeTime;
				finalUI [i].color = newColor;
			}
			Color newHandColor = handRender.color;
			newHandColor.a += Time.deltaTime / fadeTime;
			handRender.color = newHandColor;
			foreach (GameObject go in levelStrokes){
				Material lineMat = go.GetComponent<LineRenderer>().material;
				Color newLineColor = lineMat.color;
				newLineColor.a -= Time.deltaTime / fadeTime;
				lineMat.color = newLineColor;
				foreach (SpriteRenderer sr in go.GetComponentsInChildren<SpriteRenderer>()){
					Color newSpriteColor = sr.color;
					newSpriteColor.a -= Time.deltaTime / fadeTime;
					sr.color = newSpriteColor;
				}
			}
			if (finalUI[0].color.a <= 0){
				ResetFinalUI();
			}
		}
	}

	void OnDrawGizmos(){
		Gizmos.DrawIcon(transform.position, "LevelEditor.png");
	}
}
[System.Serializable]
public class Stroke {
	public BezierCurve curve;
	public float duration;
	public BrushTip.Direction direction;
	public bool automatic;
}