using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour {
	public Camera cam;
	public float movespeed;
	public float dashspeed;
	public float jumppower;
    public float airdrag;
	int environmentmask;

	Rigidbody rb;
	Animator animator;
	float h,v;
	bool dashing = false;
	bool jumping = false;
	bool onGround;
	Vector3 movevector;
    Vector3 jumpvector;
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

		if (IsGrounded () && Input.GetKeyDown (KeyCode.Space)) {
			jumping = true;
		} else
			jumping = false;
		
		if (Input.GetKey (KeyCode.LeftShift)) {
			dashing = true;
		} else
			dashing = false;

		onGround = IsGrounded ();
		Debug.Log (onGround);

		Move (h, v, dashing, jumping);
	}

	public void Move(float h, float v, bool dash,bool jump){
		//set movespeed
		float _movspd = movespeed;
		if (dash) {
			_movspd = movespeed + dashspeed;
		}

        movevector = cam.transform.forward * v * _movspd + cam.transform.right * h * _movspd;
        movevector.y = 0f;

        //ground movement
        if (IsGrounded())
        {
            //movevector = cam.transform.forward * v * _movspd + cam.transform.right * h * _movspd;

            if (jump)
            {
                //movevector.y += jumppower;
                jumpvector = Vector3.up * jumppower;
            }
            else
            {
                //movevector.y = 0f;
                jumpvector = Vector3.zero;
            }

            float _amovspd = movevector.normalized.magnitude;
            animator.SetFloat("movespeed", _amovspd);
        }
        else {
            movevector -= movevector * airdrag;
        }

		//character rotation
		//if (movevector != Vector3.zero && !jump) {
        if (movevector != Vector3.zero) {
            Vector3 _r = movevector;
			_r.y = 0f;
            float _rad = IsGrounded() ? 1f : airdrag;
            _r *= _rad;
            Quaternion _rot = Quaternion.LookRotation (_r * Time.fixedDeltaTime);
			rb.MoveRotation (_rot);
		}

		//apply movement
		rb.MovePosition (transform.position + (movevector + jumpvector) * Time.fixedDeltaTime);

		animator.SetBool ("onDash", dash);
	}

	public bool IsGrounded(){
		return Physics.Raycast (transform.position, -Vector3.up, 0.1f, environmentmask);
	}
}
