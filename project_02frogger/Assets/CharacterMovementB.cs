using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovementB : MonoBehaviour {

	public float movespeed;
	public float turnspeed;

	Rigidbody rb;
	Animator anim;
	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody> ();
		//anim = GetComponentInChildren<Animator> ();

	}
	
	// Update is called once per frame
	void FixedUpdate () {
		float translation = Input.GetAxis ("Vertical") * movespeed * Time.fixedDeltaTime;
		float rotation = Input.GetAxis ("Horizontal") * turnspeed * Time.fixedDeltaTime;

		Quaternion _turn = Quaternion.Euler (0f, rotation, 0f);
		rb.MoveRotation (rb.rotation * _turn);
	}
}
