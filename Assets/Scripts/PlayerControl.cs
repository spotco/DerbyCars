using UnityEngine;
using System.Collections;

public class PlayerControl : MonoBehaviour {

	public GameObject car;

	GameObject userInterface;
	SteeringUI steeringUI;

	float steering;
	bool isDrifting = false;
	float timer;
	float delayTime = 2.0f;
	int wheelRotDirection = 0;

	// Use this for initialization
	void Start () {
		userInterface = GameObject.Find("UnityWatermark-small");
		timer = delayTime;
	}
	
	// Update is called once per frame
	void Update () {

		steeringUI = userInterface.GetComponent<SteeringUI>();
		steering = steeringUI.angle;

		//  Key Control
		if (Input.GetKey ("w")) {
			transform.Translate(0, 0, 0.1f);
		}
		if (Input.GetKey ("s")) {
			transform.Translate(0, 0, -0.1f);
		}
		if (Input.GetKey ("d") && (Input.GetKey ("w") || Input.GetKey ("s"))) {
			transform.Rotate(0, 5.0f, 0);
		}
		if (Input.GetKey ("a") && (Input.GetKey ("w") || Input.GetKey ("s"))) {
			transform.Rotate(0, -5.0f, 0);
		}

		if(Input.GetKeyDown("e")){
			isDrifting = true;
			if(steering > 0){
				wheelRotDirection = 1;
			}else{
				wheelRotDirection = -1;
			}
		}

		Quaternion lookRot = Quaternion.LookRotation(transform.forward);

		if (wheelRotDirection == 1) {
			if (steering < 0) {
				isDrifting = false;
			}
		} else if (wheelRotDirection == -1) {
			if (steering > 0) {
				isDrifting = false;	
			}
		}

		// Simulating steering wheel's value
//		if (Input.GetKey ("7")) {
//				steering = 360.0f;
//		} else if (Input.GetKey ("8")) {
//				steering = 180.0f;
//		} else if (Input.GetKey ("9")) {
//				steering = -180.0f;
//		} else if (Input.GetKey ("0")) {
//				steering = -360.0f;
//		} else {
//			steering = 0;		
//		}

		// Wheel Control
		if ((steering > 0 || steering < 0) && (Input.GetKey ("w") || Input.GetKey ("s"))) {
			if(isDrifting){
				car.transform.Rotate(0, 1.0f * (steering / 360), 0);
			}else{
				transform.Rotate(0, 1.0f * (steering / 360), 0);
			}
		}

	}
}
