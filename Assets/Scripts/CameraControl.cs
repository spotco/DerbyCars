using UnityEngine;
using System.Collections;

public class CameraControl : MonoBehaviour {

	public GameObject car;

	GameObject userInterface;
	SteeringUI steeringUI;

	float steering;

	// Use this for initialization
	void Start () {
		userInterface = GameObject.Find("UnityWatermark-small");
	}
	
	// Update is called once per frame
	void Update () {

		steeringUI = userInterface.GetComponent<SteeringUI>();
		steering = steeringUI.angle;

		// when the car reduces speed, the camera zoom in
		if (steering > 20.0f || steering < -20.0f) {

		} else {

		}


	}
}
