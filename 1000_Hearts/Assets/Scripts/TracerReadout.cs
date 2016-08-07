using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TracerReadout : MonoBehaviour {
	public BrushTip brush;

	private Text text;
	private string textString;
	private string readout;
	
	// Use this for initialization
	void Start () {
		text = GetComponent<Text>() as Text;
		textString = text.text;
		readout = "0";
	}
	
	// Update is called once per frame
	void Update () {
		readout = brush.AverageDistance.ToString();
		text.text=textString+readout;
	}
}
