using UnityEngine;
using System.Collections;

public class EmitIfMoving : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (this.transform.parent.rigidbody.velocity.magnitude > 5) {
			this.GetComponent<ParticleEmitter>().maxEnergy = 1.2f;
		} else {
			this.GetComponent<ParticleEmitter>().maxEnergy = 0;
		}

	}
}
