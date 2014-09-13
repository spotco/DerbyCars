using UnityEngine;
using System.Collections;

public class SteeringUI : MonoBehaviour {

	bool steering = false;
	public float angle = 0;
	float startAngle = 0;
	float startSteeringAngle = 0;

	// Use this for initialization
	void Start () {
		UpdateSettings();
	}

	// Settings
	Rect rect;
	Rect screen;
	void UpdateSettings() {
		rect = new Rect(0, 0, 100, 100);
		screen = new Rect(250, 250, 500, 500);

		ProcessedSettings();
	}

	// Variables processed from settings
	Vector2 pivot;
	void ProcessedSettings() {
		pivot = new Vector2(rect.x+rect.width/2, rect.y+rect.height/2);
	}
	
	// Update is called once per frame
	void Update () {
		if (Application.isEditor) { UpdateSettings(); }
	}

	void OnGUI () {

		GUI.BeginGroup(screen);
		Vector2 cursorPosition = Event.current.mousePosition;

		if (Input.GetMouseButtonDown(0)) {
			// Clicked on the steering wheel.
			if (Vector2.Distance(pivot, cursorPosition) < 100) {
				steering = true;
				startAngle = angle;
				startSteeringAngle = Mathf.Atan2(cursorPosition.y - pivot.y, cursorPosition.x - pivot.x) * Mathf.Rad2Deg;
			}

		}


		if (Input.GetMouseButtonUp(0)) {
			steering = false;
		}

		if (steering) {
			float newSteeringAngle = Mathf.Atan2(cursorPosition.y - pivot.y, cursorPosition.x - pivot.x) * Mathf.Rad2Deg;
			float deltaSteeringAngle = Mathf.DeltaAngle(startSteeringAngle, newSteeringAngle);

			angle = startAngle + deltaSteeringAngle;

			angle = Mathf.Min(angle, 360);
			angle = Mathf.Max(angle, -360);

			// this allows us to spin the wheel multiple times
			startAngle = angle;
			startSteeringAngle = newSteeringAngle;
		} else {
			if (angle > 0) {
				angle -= Time.deltaTime*300;
				if (angle < 0) { angle = 0; }
			} else if (angle < 0) {
				angle += Time.deltaTime*300;
				if (angle > 0) { angle = 0; }
			}
		}

		GUIUtility.RotateAroundPivot(angle, pivot);
		GUI.DrawTexture(rect, guiTexture.texture);

		GUI.EndGroup();
	}
}
