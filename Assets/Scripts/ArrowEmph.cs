using UnityEngine;
using System.Collections;

public class ArrowEmph : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}

	float _theta = 0;
	Vector3 _apos;
	// Update is called once per frame
	void Update () {
		
		this.transform.Rotate(0,0,1.5f,Space.Self);


	}

	void LateUpdate() {
		Vector3 apos = this.transform.position;
		apos.y -= Mathf.Sin(_theta-0.025f) * 0.1f;

		_apos = apos;

		Vector3 npos = this.transform.position;
		npos.y += Mathf.Sin(_theta) * 0.1f;
		this.transform.position = npos;

		_theta+=0.025f;
	}
}
