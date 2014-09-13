using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CarControlScript : MonoBehaviour {

	public Rigidbody _body;
	public GameObject _backwards, _up;

	public SteeringUI steeringUI;

	void Start () {
		_body = gameObject.GetComponent<Rigidbody>();
		_backwards = Util.FindInHierarchy(this.gameObject,"Backwards");
		_up = Util.FindInHierarchy(this.gameObject,"Up");
	}


	void Update () {
		if (Input.GetKey(KeyCode.Space)) {
			if (_instanceid_to_collision_normal.Count > 0 && is_flat()) {
				Vector3 move_dir = Util.vec_scale(Util.vec_sub(this.gameObject.transform.position,_backwards.transform.position).normalized,35);
				//move_dir = Util.vec_scale(move_dir,50);
				//_body.AddForce(move_dir);
				_body.velocity = move_dir;
			}

		}

		float steering = steeringUI.angle;
		if (Mathf.Abs(steering) > 1) {
			if (_instanceid_to_collision_normal.Count > 0 && is_flat()) {
				_body.transform.Rotate(0,0,10 * steering/360);
			}
		}


		if (Input.GetKey(KeyCode.R)) {
			this.transform.position = Util.vec(-4.657434f,-23.28292f,-78.73008f);
			this.transform.eulerAngles = new Vector3(270,0,0);
			_body.angularVelocity = Vector3.zero;
			_body.velocity = Vector3.zero;
		}

	}

	public float get_from_flat_angle() {
		float angle_r = 3;
		foreach (Vector3 normal in _instanceid_to_collision_normal.Values) {
			Vector3 up_dir = Util.vec_sub(this.gameObject.transform.position,_up.transform.position).normalized;
			float dot = normal.x * up_dir.x + normal.y * up_dir.y + normal.z * up_dir.z;
			dot /= normal.magnitude;
			angle_r = Mathf.Acos(dot);
			return angle_r;
		}
		return angle_r;
	}

	public bool is_flat() {
		return get_from_flat_angle() > 1.65f || float.IsNaN(get_from_flat_angle());
	}
		
	public Dictionary<int,Vector3> _instanceid_to_collision_normal = new Dictionary<int, Vector3>();
	void OnCollisionEnter(Collision col) {
		ContactPoint contact = col.contacts[0];
		_instanceid_to_collision_normal[col.collider.GetInstanceID()] = contact.normal;
	}

	void OnCollisionExit(Collision col) {
		if (_instanceid_to_collision_normal.ContainsKey(col.collider.GetInstanceID()))
			_instanceid_to_collision_normal.Remove(col.collider.GetInstanceID());
	}
}
