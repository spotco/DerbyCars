using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PathMaker : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}

	List<Transform> children;
	void UpdateSettings() {
		children = new List<Transform>();

		foreach (Transform child in transform) {
			children.Add(child);
		}
	}
	
	// Update is called once per frame
	void Update () {
		if (Application.isEditor) { UpdateSettings(); }
	}

	// Modified Bezier Curve
	void Curve() {

	}

	static int sectors = 10;
	void DrawCurve(Vector3 start, Vector3 end) {

		Vector3 last = start;
		for (int i = 1; i < sectors; i++) {
			float interval = i / sectors;

			Vector3 delta = end-last;
			// Gizmos.DrawLine(last, );
		}
	}

	void OnSceneGUI() {

	}

	void OnDrawGizmos() {
		if (Application.isEditor) { UpdateSettings(); }

		for (int i = 0; i < children.Count; i++) {
			Transform child = children[i];
			var nextChild = children[(i+1) % children.Count];

	        Gizmos.color = Color.yellow;
	        Gizmos.DrawLine(child.position, nextChild.position);

	        var delta = nextChild.position - child.position;
	        var midPoint = child.position + delta/2;

	        Vector3 up = new Vector3(0, 1, 0);
	        Vector3 perp = Vector3.Cross(delta, up).normalized;

	        Gizmos.DrawLine(midPoint, midPoint - delta.normalized*1 + perp);
	        Gizmos.DrawLine(midPoint, midPoint - delta.normalized*1 - perp);

			// DrawCurve(child.position, nextChild.position);

	        // Handles.DrawBezier(child.position, nextChild.position, nextChild.position, nextChild.position, Color.red, null, 3);
		}
	}
}
