using UnityEngine;
using System.Collections;

public class FollowPlayerCar : MonoBehaviour {

	
	[SerializeField]public GameObject _follow;

	GameObject _up;
	GameObject _backwards;
	CarControlScript _control;

	void Start () {
		_up = Util.FindInHierarchy(_follow,"Up");
		_backwards = Util.FindInHierarchy(_follow,"Backwards");
		_control = _follow.GetComponent<CarControlScript>();
	}

	float _pct_to_away_cam = 0;
	float _in_air_ct = 0;
	void Update () {
		if (_control._instanceid_to_collision_normal.Count == 0) {
			_in_air_ct++;
		} else {
			_in_air_ct/=2;
		}

		if (_control.get_from_flat_angle()<1.9f || _in_air_ct > 20) {
			_pct_to_away_cam =_pct_to_away_cam +  (1-_pct_to_away_cam)/15.0f;
		} else {
			_pct_to_away_cam *= 0.96f;
		}

		Vector3 away_cam = Util.vec_add(
			_follow.transform.position,
			Util.vec_add(
				Util.vec(0,20,0),
				Util.vec_scale(Util.vec_sub(_backwards.transform.position,_follow.transform.position).normalized,35)
			)
		);

		Vector3 back_cam = Util.vec_add(
			_follow.transform.position,
			Util.vec_add(
				Util.vec_scale(Util.vec_sub(_up.transform.position,_follow.transform.position).normalized,10),
				Util.vec_scale(Util.vec_sub(_backwards.transform.position,_follow.transform.position).normalized,30)
			)
		);

		back_cam.x = (away_cam.x - back_cam.x)*_pct_to_away_cam + back_cam.x;
		back_cam.y = (away_cam.y - back_cam.y)*_pct_to_away_cam + back_cam.y;
		back_cam.z = (away_cam.z - back_cam.z)*_pct_to_away_cam + back_cam.z;
		this.gameObject.transform.position = back_cam;
		this.gameObject.transform.LookAt(_follow.transform.position,new Vector3(0,1,0));
	}
}
