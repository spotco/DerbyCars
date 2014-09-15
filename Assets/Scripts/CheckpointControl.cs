using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CheckpointControl : MonoBehaviour {
	
	public static CheckpointControl inst;

	Transform myTransform;	
	public float timer = 0.0f;	
	GameObject[] checkpointLocations;

	public int score = 0;

	public void reset() {
		timer = 40;
		timer_ct = 20;
		score = 0;
	}

	void Start () {
		inst = this;

		myTransform = transform;

		checkpointLocations = GameObject.FindGameObjectsWithTag("Checkpoint");

		myTransform.position = checkpointLocations[Util.rand.Next(checkpointLocations.Length)].transform.position;

		reset();
	}

	void Update () {
		timer -= Time.deltaTime;				

		collided_ct--;
	}

	int collided_ct = 0;
	int timer_ct;


	void OnTriggerEnter(Collider col) {
		if (col.tag == "Player" && collided_ct <= 0) {
			timer += timer_ct;
			collided_ct = 5;
			col.GetComponent<CarControlScript>()._start_pos = myTransform.position;

			GameObject plustime = (GameObject)Instantiate(Resources.Load("prefabs/Plustime"));
			plustime.GetComponent<TextMesh>().text = "Time + "+timer_ct;

			timer_ct = Mathf.Max(5,timer_ct-1);

			plustime.transform.position = myTransform.position;

			GameObject fireworks = (GameObject)Instantiate(Resources.Load("prefabs/Fireworks"));
			fireworks.transform.position = myTransform.position;

			myTransform.position = checkpointLocations[Util.rand.Next(checkpointLocations.Length)].transform.position;

			score++;
						AudioClip vv = Resources.Load("sfx_goal") as AudioClip;
						audio.PlayOneShot(vv);
		}
	}




}
	