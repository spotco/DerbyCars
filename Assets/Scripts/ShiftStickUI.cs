using UnityEngine;
using System.Collections;

public class ShiftStickUI : MonoBehaviour {

	enum Gear {
		None,
		One,
		Two,
		Three,
		Four,
		Five,
		Reverse
	}

	Gear gear = Gear.One;

	bool dragging = false;
	Vector2 startMousePosition;
	Vector2 startStickPosition;

	Rect stickRect;

	// Use this for initialization
	void Start () {
		stickRect = new Rect(0, 0, stickSize.x, stickSize.y);
		UpdateSettings();
	}

	// Settings
	Vector2 stickSize;
	Rect gridRect;
	Rect screen;
	void UpdateSettings() {
		stickSize = new Vector2(50, 50);
		gridRect = new Rect(0, 0, 200, 200);

		screen = new Rect(0, 0, 500, 500);

		ProcessedSettings();
	}

	// Variables processed from settings
	float threshold;
	float gearHeight;
	void ProcessedSettings() {
		stickRect.size = stickSize;
		threshold = gridRect.width / 20;
		gearHeight = gridRect.height / 4;
	}

	// Update is called once per frame
	void Update () {
		if (Application.isEditor) { UpdateSettings(); }
	}

	void OnGUI () {

		GUI.BeginGroup(screen);
		Vector2 cursorPosition = Event.current.mousePosition;

		if (Input.GetMouseButtonDown(0) && stickRect.Contains(cursorPosition)) {
			dragging = true;

			startMousePosition = cursorPosition;
			startStickPosition = stickRect.position;
		}

		if (Input.GetMouseButtonUp(0)) {
			dragging = false;

			if (stickRect.x < gridRect.width/3 - stickSize.x/2) {
				stickRect.x = stickSize.x/2 - threshold*2;
			} else if (stickRect.x > gridRect.width*2/3 - stickSize.x/2) {
				stickRect.x = gridRect.width - stickSize.x - threshold/2;
			} else {
				stickRect.x = gridRect.width/2 - stickSize.x/2 - threshold/2;
			}

			if (stickRect.y < gridRect.center.y - stickSize.y/2) {
				stickRect.y = gridRect.center.y - stickSize.y/2 - threshold - gearHeight;
			} else {
				stickRect.y = gridRect.center.y - stickSize.y/2 + threshold + gearHeight;
			}
		}

		if (dragging) {
			Vector2 deltaMousePosition = cursorPosition - startMousePosition;
			stickRect.position = startStickPosition + deltaMousePosition;

			stickRect.x = Mathf.Max(stickRect.x, 0);
			stickRect.x = Mathf.Min(stickRect.x, gridRect.width-stickRect.width);

			if (stickRect.x < threshold*2 ||
				stickRect.x > gridRect.width - stickRect.width - threshold*2 ||
				stickRect.x > gridRect.center.x - stickRect.width/2 - threshold && stickRect.x < gridRect.center.x - stickRect.width/2 + threshold) {
				stickRect.y = Mathf.Max(stickRect.y, gridRect.center.y - stickSize.y/2 - threshold - gearHeight);
				stickRect.y = Mathf.Min(stickRect.y, gridRect.center.y - stickSize.y/2 + threshold + gearHeight);
			} else {
				stickRect.y = Mathf.Max(stickRect.y, gridRect.center.y - stickSize.y/2 - threshold);
				stickRect.y = Mathf.Min(stickRect.y, gridRect.center.y - stickSize.y/2 + threshold);				
			}


		}

		GUI.Box(new Rect(0, 0, 200, 200), "hey");
		GUI.Box(stickRect, "stick");

		GUI.EndGroup();
	}
}
