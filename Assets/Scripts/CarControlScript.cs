using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CarControlScript : MonoBehaviour {
	

	public static CarControlScript inst;

	public Rigidbody _body;
	public GameObject _backwards, _up;
	public Vector3 _start_pos;
	public SteeringUI steeringUI;

	void Start () {
		_body = gameObject.GetComponent<Rigidbody>();
		_backwards = Util.FindInHierarchy(this.gameObject,"Backwards");
		_up = Util.FindInHierarchy(this.gameObject,"Up");
		inst = this;
	}

	bool hasFloor;

	public bool HasFloor() {
		return hasFloor;
	}
	void Update () {
		float steering = steeringUI.angle;
		if (_instanceid_to_collision_normal.Count > 0 && is_flat()) {
			if (Input.GetKey(KeyCode.Space) || Input.GetKey(KeyCode.W)) {
				rigidbody.AddRelativeForce(Vector3.up*-100, ForceMode.Acceleration);
				rigidbody.AddRelativeTorque(Vector3.forward*steering*0.15f, ForceMode.Acceleration);
			}
			if (Input.GetKey(KeyCode.A)) {
				rigidbody.gameObject.transform.Rotate(0,0,-3);
			}
			if (Input.GetKey(KeyCode.D)) {
				rigidbody.gameObject.transform.Rotate(0,0,3);
			}
		}

		if (Input.GetKey(KeyCode.R)) {
			reset();
		}

	}

	public void reset() {
		this.transform.position = _start_pos;
		this.transform.eulerAngles = new Vector3(270,0,0);
		_body.angularVelocity = Vector3.zero;
		_body.velocity = Vector3.zero;
	}

		
	void OnCollisionStay(Collision collisionInfo) {
		Vector3 upDir = transform.rotation * Vector3.forward;
		hasFloor = false;
		foreach(ContactPoint contact in collisionInfo.contacts) {
			float angle = Vector3.Angle(contact.normal, upDir);

			if (angle < 30.0f) {
				hasFloor = true;
			}
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

				AudioClip vv = Resources.Load("sfx_explosion") as AudioClip;
						audio.PlayOneShot(vv);

	}

	void OnCollisionExit(Collision col) {
		if (_instanceid_to_collision_normal.ContainsKey(col.collider.GetInstanceID()))
			_instanceid_to_collision_normal.Remove(col.collider.GetInstanceID());
	}
}