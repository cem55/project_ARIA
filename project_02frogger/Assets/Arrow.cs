using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour {
	public Transform target;
	Rigidbody rb;
	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody> ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void Shoot(Vector3 force){
		rb.AddForce (force);
		rb.useGravity = true;
		rb.GetComponent<Collider> ().isTrigger = false;
	}
}