using UnityEngine;
using System.Collections;

public class TimeDisp : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		this.GetComponent<TextMesh>().text = "Time: "+ Mathf.Floor(CheckpointControl.inst.timer);
		if (CheckpointControl.inst.timer < 10) {
			this.GetComponent<TextMesh>().color = Color.red;
		} else if (CheckpointControl.inst.timer < 25) {
			this.GetComponent<TextMesh>().color = Color.yellow;
		} else {
			this.GetComponent<TextMesh>().color = Color.green;
		}
	}
}
