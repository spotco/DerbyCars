using UnityEngine;
using System.Collections;

public class GameState : MonoBehaviour {
	
	public static GameState inst;
	public bool in_menu;
	public GameObject menu_cam;
	public GameObject game_cam;
	// Use this for initialization
	void Start () {
		inst = this;
		in_menu = true;
	}
	
	// Update is called once per frame
	void Update () {
		if (in_menu) {
			menu_cam.SetActive(true);
			game_cam.SetActive(false);

			if (Input.GetKeyDown(KeyCode.Space)) {
				in_menu = false;
				CheckpointControl.inst.reset();
				CarControlScript.inst._start_pos = new Vector3(332.1359f,5.745834f,-228.8224f);
				CarControlScript.inst.reset();
			}

		} else {
			menu_cam.SetActive(false);
			game_cam.SetActive(true);

			if (CheckpointControl.inst.timer <= 0) {
				in_menu = true;
			}
		}
	}
}
