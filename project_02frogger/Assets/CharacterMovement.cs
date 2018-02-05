using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour {
	public Camera cam;
	public float movespeed;
	public float dashspeed;
	public float jumppower;
	int environmentmask;

	Rigidbody rb;
	Animator animator;
	float h,v;
	bool dashing = false;
	bool jumping = false;
	bool onGround;
	Vector3 movevector;
	Vector3 lookrot;
	// Use this for initialization
	void Start () {
		if (cam == null) {
			cam = Camera.main;
		}
		rb = GetComponent<Rigidbody> ();
		animator = GetComponentInChildren<Animator> ();
		environmentmask = LayerMask.GetMask ("environment");
	}

	void FixedUpdate () {
		h = Input.GetAxis ("Horizontal");
		v = Input.GetAxis ("Vertical");

		if (isGrounded () && Input.GetKeyDown (KeyCode.Space)) {
			jumping = true;
		} else
			jumping = false;
		
		if (Input.GetKey (KeyCode.LeftShift)) {
			dashing = true;
		} else
			dashing = false;

		onGround = isGrounded ();
		Debug.Log (onGround);

		Move (h, v, dashing, jumping);
	}

	public void Move(float h, float v, bool dash,bool jump){
		//set movespeed
		float _movspd = movespeed;
		if (dash) {
			_movspd = movespeed + dashspeed;
		}

		//ground movement
		if (isGrounded ()) {
			movevector = cam.transform.forward * v * _movspd + cam.transform.right * h * _movspd;

			if (jump) {
				movevector.y += jumppower;
			} else
				movevector.y = 0f;

			float _amovspd = movevector.normalized.magnitude;
			animator.SetFloat ("movespeed", _amovspd);
		}

		//character rotation
		if (movevector != Vector3.zero && !jump) {
			Vector3 _r = movevector;
			_r.y = 0f;
			Quaternion _rot = Quaternion.LookRotation (_r);
			rb.MoveRotation (_rot);
		}

		//apply movement
		rb.MovePosition (transform.position + movevector * Time.fixedDeltaTime);

		animator.SetBool ("onDash", dash);
	}

	public bool isGrounded(){
		return Physics.Raycast (transform.position, -Vector3.up, 0.1f, environmentmask);
	}
}
