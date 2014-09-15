using UnityEngine;
using System.Collections;

public class CheckpointArrow : MonoBehaviour {

	public GameObject _to;

	void Update () {
		this.transform.LookAt(_to.transform.position,new Vector3(0,1,0));
	}
}
