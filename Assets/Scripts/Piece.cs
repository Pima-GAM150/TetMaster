using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Piece : MonoBehaviour {

	public List<Transform> blocks;
	public Rigidbody2D rbody;

	public Spawner spawner;
	public Controller controller;
	public Tracker tracker;

	bool grounded = false;

	void FixedUpdate() {
		if( rbody.velocity.y > -0.1f && !grounded ) {
			tracker.CountRowFor( this );

			if( controller.currentPiece == this ) {
				spawner.Spawn();
			}

			grounded = true;
			rbody.velocity = Vector2.zero;
		}
		
		if( rbody.velocity.y != 0f && grounded ) {
			grounded = false;
		}
	}
}
