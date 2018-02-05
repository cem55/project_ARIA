using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraHandler : MonoBehaviour {
	//public bool cursorlock = true;
	public Transform target;
	public float sensitivityX;
	public float sensitivityY;
	public float distance;
	public float minTiltY = -10f;
	public float maxTiltY = 90f;

	float h;
	float v;
	float _h;
	float _v;
	float _d;
	// Use this for initialization
	void Start () {
		_h = 0f;
		_v = 0f;
		Cursor.lockState = CursorLockMode.Locked;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		h += Input.GetAxis ("Mouse X") * sensitivityY;
		v -= Input.GetAxis ("Mouse Y") * sensitivityX;

		v = Mathf.Clamp (v, minTiltY, maxTiltY);

		_h = Mathf.LerpAngle (_h, h, 0.1f);
		_v = Mathf.LerpAngle (_v, v, 0.1f);
		_d = -distance;

		//transform.eulerAngles = new Vector3(v,h,0f);
		transform.eulerAngles = new Vector3(_v,_h,0f);
		//Vector3 _looktoangle = new Vector3(v,h,0f);
		//transform.eulerAngles = Vector3.Slerp (transform.eulerAngles, _looktoangle, 0.1f);

		//transform.LookAt (target);
		transform.position = target.position + transform.forward * _d;
		//Vector3 _destinationvector = target.position + transform.forward * _d;
		//transform.position = Vector3.Slerp (transform.position, _destinationvector, 0.1f);
	}
}
