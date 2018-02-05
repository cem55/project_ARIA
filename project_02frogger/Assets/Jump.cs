using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jump : MonoBehaviour {

	public float power;

	Rigidbody rb;
	float angle;
	float lookangle;

	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody> ();
		angle = 0f;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		if (Input.GetButton ("Fire1")) {
			angle++;
			lookangle = angle;

			angle = Mathf.Clamp (angle, 0f, 60f);
		}
		Debug.Log ("angle : " + angle);
		Vector3 jumpvector = new Vector3 (0f, Mathf.Cos (angle), 1f) * power;

		if (Input.GetButtonUp ("Fire1")) {
			transform.eulerAngles = new Vector3 (-lookangle, 0f, 0f);
			rb.AddForce (jumpvector, ForceMode.Impulse);
			angle = 0f;
		}

		if (lookangle > 0.1f) {
			lookangle--;
		} else
			lookangle = 0f;

		//transform.eulerAngles = new Vector3 (lookangle, 0f, 0f);
	}
}
