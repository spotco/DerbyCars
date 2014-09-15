using UnityEngine;
using System.Collections;

public class LookAt : MonoBehaviour {
	
	public GameObject at;

	// Use this for initialization
	void Start () {
		at = GameObject.FindGameObjectWithTag("Cam");
		this.transform.LookAt(at.transform,new Vector3(0,1,0));
				this.transform.Rotate(0,180,0);
	}
	
	// Update is called once per frame
	void Update () {
		this.transform.LookAt(at.transform,new Vector3(0,1,0));
		this.transform.Rotate(0,180,0);
	}
}
