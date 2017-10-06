using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour {

	public Controller controller;
	public Tracker tracker;

	public Piece[] piecesPrefabs;

	public float spawnY;
	public float spawnX;
	public float spawnXRange;

	public float startingVelocity;
	public float dragRate;
	public float dragReductionRate;

	void Start() {
		Spawn();
	}

	public void Spawn() {
		Piece newPiece = Instantiate<Piece>( piecesPrefabs[ Random.Range( 0, piecesPrefabs.Length ) ] );
		newPiece.transform.position = new Vector3( spawnX + Mathf.Round( Random.Range( -spawnXRange, spawnXRange ) ), spawnY, 0f );
		newPiece.rbody.velocity = new Vector2( 0f, startingVelocity );
		newPiece.rbody.drag = dragRate;
		newPiece.spawner = this;
		newPiece.tracker = tracker;
		newPiece.controller = controller;

		tracker.AddPiece( newPiece );

		controller.TakeControlOf( newPiece );
	}

	void Update() {
		if( dragRate > 0f ) dragRate -= dragReductionRate * Time.deltaTime;
		else dragRate = 0f;
	}
}
